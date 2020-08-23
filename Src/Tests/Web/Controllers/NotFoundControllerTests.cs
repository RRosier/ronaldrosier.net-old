using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rosier.Blog.Web.Controllers;
using Moq;
using Rosier.Blog.Services;
using System.Web.Mvc;

namespace Rosier.Blog.Web.Tests.Controllers
{
    [TestFixture]
    public class NotFoundControllerTests : ControllerTestsBase<NotFoundController>
    {
        [Test]
        public void Index()
        {
            const string expectedViewName = "NotFound";
            var action = controller.Index() as ViewResult;

            Assert.NotNull(action);
            Assert.AreEqual(expectedViewName, action.ViewName);
        }
    }
}
