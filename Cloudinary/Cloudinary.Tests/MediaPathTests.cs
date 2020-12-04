using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cloudinary.Tests
{
    [TestClass]
    public class MediaPathTests
    {
        private readonly HtmlTranformer htmlTranformer = new HtmlTranformer("https://res.cloudinary.com/test/image/upload/media/", "f_auto");

        [TestMethod]
        public void TestMedia()
        {
            var html = "<img src=\"/media/3354/valley-park-farm.jpg\" />";
            var transformedHtml = htmlTranformer.TransformMediaLinks(html);
            var expectedHtml = "<img src=\"https://res.cloudinary.com/test/image/upload/media/3354/valley-park-farm.jpg\" />";
            Assert.IsTrue(transformedHtml == expectedHtml, "Transformed is " + transformedHtml);
        }

        [TestMethod]
        public void TestMediaCrop()
        {
            var html = "<img src=\"/media/{{crop-600/{{m}}/3354/valley-park-farm.jpg\" />";
            var transformedHtml = htmlTranformer.TransformMediaLinks(html);
            var expectedHtml = "<img src=\"https://res.cloudinary.com/test/image/upload/f_auto,w_600/media/3354/valley-park-farm.jpg\" />";
            Assert.IsTrue(transformedHtml == expectedHtml, "Transformed is " + transformedHtml);
        }
    }
}
