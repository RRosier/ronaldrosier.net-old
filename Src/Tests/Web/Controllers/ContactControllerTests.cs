using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Web.Controllers;
using System.Web.Mvc;
using Xunit;

namespace Rosier.Blog.Web.Tests.Controllers
{
    public class ContactControllerTests : ControllerTestsBase<ContactController>
    {
        [Fact]
        public void Index()
        {
            const string expectedViewName = "";
            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(expectedViewName, result.ViewName);
        }
    }
}
