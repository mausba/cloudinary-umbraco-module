using System;
using System.Web;

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
            var app = (HttpApplication)sender;
            var http = new HttpContextWrapper(app.Context);

            var filter = new ResponseFilterStream(http.Response.Filter);
            filter.TransformString += filter_TransformString;
            http.Response.Filter = filter;
        }

        private string filter_TransformString(string arg)
        {
            return htmlTranformer.TransformMediaLinks(arg);
        }

        public void Dispose() { }
    }
}
