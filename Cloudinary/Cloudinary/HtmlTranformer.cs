using System.Configuration;

namespace Cloudinary
{
    public class HtmlTranformer
    {
        private readonly string cloudinaryUrl;
        private readonly string cloudinaryCropFormat;

        public HtmlTranformer() : this(ConfigurationManager.AppSettings["CloudinaryUrl"], ConfigurationManager.AppSettings["CloudinaryCropFormat"]) { }

        public HtmlTranformer(string cloudinaryUrl, string cloudinaryCropFormat)
        {
            this.cloudinaryUrl = cloudinaryUrl;
            this.cloudinaryCropFormat = cloudinaryCropFormat;
        }

        public string TransformMediaLinks(string html)
        {
            html = html.Replace("/media/", cloudinaryUrl);
            html = html.Replace("/media/{{crop-", "/" + cloudinaryCropFormat + ",w_");
            html = html.Replace("{{m}}", "media");
            return html;
        }
    }
}
