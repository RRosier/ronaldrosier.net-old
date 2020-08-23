using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rosier.Blog.Web.Controllers;
using Moq;
using Rosier.Blog.Services;

namespace Rosier.Blog.Web.Tests.Controllers
{
    public abstract class ControllerTestsBase<T>
        where T:BaseController
    {
        protected T controller;
        protected Mock<IBlogService> serviceMock;

        [TestFixtureSetUp]
        public void TestSetUp()
        {
            this.serviceMock = new Mock<IBlogService>();
            controller = (T)Activator.CreateInstance(typeof(T), this.serviceMock.Object);
        }

        [TestFixtureTearDown]
        public void TestTearDown()
        {
            controller = null;
            serviceMock = null;
        }
    }
}
