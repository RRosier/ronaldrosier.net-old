using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Rosier.Blog.Services;

namespace Rosier.Blog.Web.Controllers
{
    /// <summary>
    /// Base class for all controllers.
    /// </summary>
    public abstract class BaseController : Controller
    {
        protected IBlogService blogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        protected BaseController(IBlogService service)
        {
            if (service == null)
                throw new ArgumentNullException("service");

            this.blogService = service;

            ViewBag.ActivePage = "Blog";
        }

        /// <summary>
        /// Commons the data.
        /// </summary>
        protected void CommonData()
        {
            ViewBag.DisplayCommentLink = false;
            ViewData["Archive"] = blogService.GetArchiveList();
            ViewData["TagCloud"] = blogService.GetTagCloud();
        }

    }
}