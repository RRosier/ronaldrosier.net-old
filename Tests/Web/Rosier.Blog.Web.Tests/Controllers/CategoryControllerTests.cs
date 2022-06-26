using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rosier.Blog.Web.Controllers;
using Rosier.Blog.Services;
using Moq;
using System.Web.Mvc;
using Rosier.Blog.Service.ViewModel;

namespace Rosier.Blog.Web.Tests.Controllers
{
    [TestFixture]
    public class CategoryControllerTests : ControllerTestsBase<CategoryController>
    {

        [Test]
        public void Index_Returns_NotFound_View()
        {
            const string expectedViewName = "NotFound";

            var result = this.controller.Index() as ViewResult;

            Assert.IsNotNull(result, "Expected a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "Expected viewname was '{0}' but returned '{1}'", expectedViewName, result.ViewName);
        }

        [Test]
        public void Listing_EmptyList_ReturnsEmptyView()
        {
            const string expectedViewName = "_Empty";
            var categoryValue = "aspnetmvc";
            this.serviceMock.Setup(s => s.GetEntriesByCategory(categoryValue, 1)).Returns(new List<BlogItemViewModel>());

            var result = this.controller.Listing(categoryValue) as ViewResult;

            Assert.IsNotNull(result, "Expected a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "Expected viewname was '{0}' but returned '{1}'", expectedViewName, result.ViewName);
        }

        [Test]
        public void Listing_Non_Empty_List_Returns_Listing_View()
        {
            const string expectedViewName = "Listing";
            const string expectedTitle = "Ronald Rosier.NET - aspnetmvc Listing (page 1)";
            const string categoryValue = "aspnetmvc";
            var list = new List<BlogItemViewModel>() { new BlogItemViewModel() };
            this.serviceMock.Setup(s => s.GetEntriesByCategory(categoryValue, 1))
                .Returns(list);

            var result = this.controller.Listing(categoryValue) as ViewResult;

            Assert.IsNotNull(result, "Expected a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "Expected viewname was '{0}' but returned '{1}'", expectedViewName, result.ViewName);
            Assert.AreEqual(list, result.Model);
            Assert.AreEqual(expectedTitle, result.ViewBag.Title);
        }
    }
}
