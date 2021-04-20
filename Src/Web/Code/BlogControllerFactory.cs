using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Rosier.Blog.Web.Controllers;
using Rosier.Blog.Services;
using Rosier.Blog.Data;
using System.ServiceModel.Channels;

namespace Rosier.Blog.Web
{
    /// <summary>
    /// Custom implementation of the controller factory
    /// </summary>
    public class BlogControllerFactory : DefaultControllerFactory
    {
        private const string blogControllerName = "blog";
        private const string contactControllerName = "contact";
        private const string categoryControllerName = "category";
        private const string errorControllerName = "error";
        private const string aboutControllerName = "about";

        /// <summary>
        /// Creates the specified controller by using the specified request context.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>
        /// The controller.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="requestContext"/> parameter is null.</exception>
        ///   
        /// <exception cref="T:System.ArgumentException">The <paramref name="controllerName"/> parameter is null or empty.</exception>
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (string.IsNullOrEmpty(controllerName))
                throw new ArgumentNullException("controllerName");

            var type = GetControllerType(requestContext, controllerName);
            if (type.BaseType == typeof(BaseController))
            {
                var unitofwork = new EntityUnitOfWork(new EntityDataContext().ObjectContext);
                var service = new BlogService(unitofwork);

                var controller = (IController)Activator.CreateInstance(type, service);
                return controller;
            }
            else
            {
                try
                {
                    return base.CreateController(requestContext, controllerName);
                }
                catch (HttpException)
                {   // controller not found or not implementing IController
                    var unitofwork = new EntityUnitOfWork(new EntityDataContext().ObjectContext);
                    var service = new BlogService(unitofwork);
                    return new NotFoundController(service);
                }
            }
        }


        /// <summary>
        /// Retrieves the controller type for the specified name and request context.
        /// </summary>
        /// <param name="requestContext">The context of the HTTP request, which includes the HTTP context and route data.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>
        /// The controller type.
        /// </returns>
        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            switch(controllerName)
            {
                case blogControllerName: return typeof(BlogController);
                case categoryControllerName: return typeof(CategoryController);
                case aboutControllerName: return typeof(AboutController);
                case contactControllerName: return typeof(ContactController);
                case errorControllerName: return typeof(ErrorController);
                default: return base.GetControllerType(requestContext, controllerName);
            }
        }
    }
}