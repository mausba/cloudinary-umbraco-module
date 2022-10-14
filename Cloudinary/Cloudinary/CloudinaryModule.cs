using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Cloudinary
{
    public class CloudinaryModule : IHttpModule
    {
        private readonly string ImageFormats = @"\/media\/(,w_\d+)?(,h_\d+)?(,x_\d+)?(,y_\d+)?(,c_[a-zA-Z]+)?\/?(.[^ ]*\.(?:avif|gif|jpeg|jpg|png|svg|webp))";
        private readonly string VideoFormats = @"\/media\/(,w_\d+)?(,h_\d+)?(,x_\d+)?(,y_\d+)?(,c_[a-zA-Z]+)?\/?(.[^ ]*\.(?:3gp|avi|mp4|mpeg|ogg|webm|wmv))";
        private readonly string CloudinaryUrl;

        public CloudinaryModule()
        {
            CloudinaryUrl = ConfigurationManager.AppSettings["CloudinaryUrl"];
        }

        public void Init(HttpApplication context)
        {
            context.PostRequestHandlerExecute += HandleRequest;
        }

        private void HandleRequest(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CloudinaryUrl) || CloudinaryUrl.Contains("{youraccount}")) return;

            var app = (HttpApplication)sender;
            var http = new HttpContextWrapper(app.Context);

            var isPage = app.Context.CurrentHandler is MvcHandler;
            var isUmbracoPath = app.Request.FilePath.StartsWith("/umbraco/");
            if (!isPage || isUmbracoPath) return;

            var filter = new ResponseFilterStream(http.Response.Filter, http);
            filter.TransformString += TransformString;
            http.Response.Filter = filter;
        }

        private string TransformString(string html)
        {
            var responseHtml = Regex.Replace(html, ImageFormats, CloudinaryUrl, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return Regex.Replace(responseHtml, VideoFormats, CloudinaryUrl.Replace("image", "video"), RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }

        public void Dispose()
        {
            // Method intentionally left empty.
        }
    }
}