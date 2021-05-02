using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Web.Controllers;
using Moq;
using Rosier.Blog.Services;

namespace Rosier.Blog.Web.Tests.Controllers
{
    public abstract class ControllerTestsBase<T> : IDisposable
        where T:BaseController
    {
        protected T controller;
        protected Mock<IBlogService> serviceMock;

        public ControllerTestsBase()
        {
            this.serviceMock = new Mock<IBlogService>();
            controller = (T)Activator.CreateInstance(typeof(T), this.serviceMock.Object);
        }

        public void Dispose()
        {
            controller = null;
            serviceMock = null;
        }
    }
}
