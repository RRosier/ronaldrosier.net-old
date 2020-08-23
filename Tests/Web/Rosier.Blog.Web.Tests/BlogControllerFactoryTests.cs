using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rosier.Blog.Web.Controllers;

namespace Rosier.Blog.Web.Tests
{
    [TestFixture]
    public class BlogControllerFactoryTests
    {
        private BlogControllerFactory factory; 

        [TestFixtureSetUp]
        public void TestSetUp()
        {
            factory = new BlogControllerFactory();
        }

        [TestFixtureTearDown]
        public void TestTearDown()
        {
        }

        [TestCase("blog", typeof(BlogController))]
        [TestCase("contact", typeof(ContactController))]
        [TestCase("category", typeof(CategoryController))]
        [TestCase("about", typeof(AboutController))]
        [TestCase("error", typeof(ErrorController))]
        [TestCase("", typeof(ArgumentNullException), ExpectedException=typeof(ArgumentNullException))]
        public void Create_BlogController(string controllerName, Type expectedtype)
        {
            var controller = factory.CreateController(new System.Web.Routing.RequestContext(), controllerName);

            Assert.AreEqual(expectedtype, controller.GetType());
        }
    }
}
