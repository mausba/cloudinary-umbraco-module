using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace Cloudinary
{
    public class CloudinaryModule : IHttpModule
    {
        private readonly HtmlTranformer htmlTranformer = new HtmlTranformer();

        public void Init(HttpApplication context)
        {
            context.PostRequestHandlerExecute += HandleRequest;
        }

        private void HandleRequest(object sender, EventArgs e)
        {
            var cloudinaryActive = ConfigurationManager.AppSettings["CloudinaryActive"] == "true";
            if (!cloudinaryActive) return;

            var app = (HttpApplication)sender;
            var http = new HttpContextWrapper(app.Context);

            var isPage = app.Context.CurrentHandler is MvcHandler;
            var isUmbracoPath = app.Request.FilePath.StartsWith("/umbraco/");
            if (!isPage || isUmbracoPath) return;

            var filter = new ResponseFilterStream(http.Response.Filter, http);
            filter.TransformString += FilterTransformString;
            http.Response.Filter = filter;
        }

        private string FilterTransformString(string arg)
        {
            return htmlTranformer.TransformMediaLinks(arg);
        }

        public void Dispose() { }
    }
}