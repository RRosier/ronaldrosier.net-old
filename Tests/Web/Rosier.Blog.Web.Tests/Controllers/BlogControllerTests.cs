using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rosier.Blog.Web.Controllers;
using System.Web.Mvc;
using Rosier.Blog.Service.ViewModel;
using Rosier.Blog.Services;
using Moq;
using System.Collections.ObjectModel;

namespace Rosier.Blog.Web.Tests.Controllers
{
    [TestFixture]
    public class BlogControllerTests : ControllerTestsBase<BlogController>
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_No_Service()
        {
            this.controller = new BlogController(null);//, null);
        }

        [Test]
        public void Default_Action_With_No_Entries_Return_Empty_View()
        {
            const string expectedViewName = "_Empty";
            var entriesList = new List<BlogItemViewModel>();

            this.serviceMock.Setup(s => s.GetAllEntries(1)).Returns(entriesList);

            var result = this.controller.Index() as ViewResult;

            Assert.IsNotNull(result, "Expected a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);
        }

        [Test]
        public void Default_Action_With_Entries_Return_Listing_View()
        {
            const string expectedViewName = "Listing";
            var entriesList = new List<BlogItemViewModel>()
            {
                new BlogItemViewModel()
            };

            this.serviceMock.Setup(s => s.GetAllEntries(1)).Returns(entriesList);

            var result = this.controller.Index() as ViewResult;

            Assert.IsNotNull(result, "Expected a ViewResult");
            Assert.AreEqual(expectedViewName, result.ViewName, "View name should have been {0}", expectedViewName);
        }

        [Test]
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
