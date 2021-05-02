using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Web.Controllers;
using Moq;
using Rosier.Blog.Services;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Rosier.Blog.Web.Tests.Controllers
{
    public class NotFoundControllerTests : ControllerTestsBase<NotFoundController>
    {
        [Fact]
        public void Index()
        {
            const string expectedViewName = "NotFound";
            var action = controller.Index() as ViewResult;

            Assert.NotNull(action);
            Assert.Equal(expectedViewName, action.ViewName);
        }
    }
}
