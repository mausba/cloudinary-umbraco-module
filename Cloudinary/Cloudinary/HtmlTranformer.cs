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
            return Regex.Replace(html, cloudinaryRegex, cloudinaryUrl, RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }
    }
}