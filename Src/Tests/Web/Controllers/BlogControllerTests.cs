using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Web.Controllers;
using Rosier.Blog.Service.ViewModel;
using Rosier.Blog.Services;
using Moq;
using System.Collections.ObjectModel;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Rosier.Blog.Web.Tests.Controllers
{
    public class BlogControllerTests : ControllerTestsBase<BlogController>
    {
        [Fact]
        public void Constructor_No_Service()
        {
            Assert.Throws<ArgumentNullException>(() => new BlogController(null));
        }

        [Fact]
        public void Default_Action_With_No_Entries_Return_Empty_View()
        {
            const string expectedViewName = "_Empty";
            var entriesList = new List<BlogItemViewModel>();

            this.serviceMock.Setup(s => s.GetAllEntries(1)).Returns(entriesList);

            var result = this.controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(expectedViewName, result.ViewName);
        }

        [Fact]
        public void Default_Action_With_Entries_Return_Listing_View()
        {
            const string expectedViewName = "Listing";
            var entriesList = new List<BlogItemViewModel>()
            {
                new BlogItemViewModel()
            };

            this.serviceMock.Setup(s => s.GetAllEntries(1)).Returns(entriesList);

            var result = this.controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(expectedViewName, result.ViewName);
        }

        [Fact]
        public void Default_Action_Return_Two_Items_In_Model()
        {
            //var itemList = new Collection<BlogItemViewModel>();
            //itemList.Add(new BlogItemViewModel());
            //itemList.Add(new BlogItemViewModel());
            //this.serviceMock.Setup(s => s.GetLatestEntries()).Returns(itemList);

            //var result = this.blogController.Index() as ViewResult;

            //var model = result.Model as Collection<BlogItemViewModel>;

            //Assert.IsNotNull(model, "Expected a List<BlogItemViewModel> as view model");
            //Assert.AreEqual(2, model.Count, "Expected 2 blogitems in model");
        }

    }
}
