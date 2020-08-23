using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Web.Controllers;
using NUnit.Framework;
using System.Web.Mvc;

namespace Rosier.Blog.Web.Tests.Controllers
{
    [TestFixture]
    public class AboutControllerTests : ControllerTestsBase<AboutController>
    {
        [Test]
        public void Index()
        {
            const string expectedViewName = "UnderConstruction";

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }
    }
}
