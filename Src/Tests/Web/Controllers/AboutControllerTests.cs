using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Web.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Rosier.Blog.Web.Tests.Controllers
{
    public class AboutControllerTests : ControllerTestsBase<AboutController>
    {
        [Fact]
        public void Index()
        {
            const string expectedViewName = "UnderConstruction";

            var result = controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(expectedViewName, result.ViewName);
        }
    }
}
