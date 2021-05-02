using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Web.Controllers;
using Rosier.Blog.Services;
using Moq;
using Rosier.Blog.Service.ViewModel;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Rosier.Blog.Web.Tests.Controllers
{
    public class CategoryControllerTests : ControllerTestsBase<CategoryController>
    {

        [Fact]
        public void Index_Returns_NotFound_View()
        {
            const string expectedViewName = "NotFound";

            var result = this.controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(expectedViewName, result.ViewName);
        }

        [Fact]
        public void Listing_EmptyList_ReturnsEmptyView()
        {
            const string expectedViewName = "_Empty";
            var categoryValue = "aspnetmvc";
            this.serviceMock.Setup(s => s.GetEntriesByCategory(categoryValue, 1)).Returns(new List<BlogItemViewModel>());

            var result = this.controller.Listing(categoryValue) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(expectedViewName, result.ViewName);
        }

        [Fact]
        public void Listing_Non_Empty_List_Returns_Listing_View()
        {
            const string expectedViewName = "Listing";
            const string expectedTitle = "Ronald Rosier.NET - aspnetmvc Listing (page 1)";
            const string categoryValue = "aspnetmvc";
            var list = new List<BlogItemViewModel>() { new BlogItemViewModel() };
            this.serviceMock.Setup(s => s.GetEntriesByCategory(categoryValue, 1))
                .Returns(list);

            var result = this.controller.Listing(categoryValue) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(expectedViewName, result.ViewName);
            Assert.Equal(list,result.Model);
            //Assert.Equal(expectedTitle, result.ViewBag.Title);
        }
    }
}
