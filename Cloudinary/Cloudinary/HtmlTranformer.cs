using System.Configuration;
using System.Text.RegularExpressions;

namespace Cloudinary
{
    public class HtmlTranformer
    {
        private readonly string cloudinaryUrl;
        private readonly string cloudinaryRegex;

        public HtmlTranformer() : this(
            ConfigurationManager.AppSettings["CloudinaryUrl"],
            ConfigurationManager.AppSettings["CloudinaryRegex"])
        { }

        public HtmlTranformer(string cloudinaryUrl, string cloudinaryRegex)
        {
            this.cloudinaryUrl = cloudinaryUrl;
            this.cloudinaryRegex = cloudinaryRegex;
        }

        public string TransformMediaLinks(string html)
        {
            var url = Regex.Match(html, "\.(?:avi|mp4|mpeg|ogv|webm|wmv)").Success
                ? cloudinaryUrl.Replace("image", "video")
                : cloudinaryUrl;

            return Regex.Replace(html, cloudinaryRegex, url, RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }
    }
}