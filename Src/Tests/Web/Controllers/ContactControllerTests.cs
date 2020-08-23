using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rosier.Blog.Web.Controllers;
using System.Web.Mvc;

namespace Rosier.Blog.Web.Tests.Controllers
{
    [TestFixture]
    public class ContactControllerTests : ControllerTestsBase<ContactController>
    {
        [Test]
        public void Index()
        {
            const string expectedViewName = "";
            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }
    }
}
