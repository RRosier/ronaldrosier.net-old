using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Rosier.Blog.Services;

namespace Rosier.Blog.Web.Controllers
{
    public class CategoryController : BaseController
    {
        public CategoryController(IBlogService service):base(service)
        {
        }

        //
        // GET: /Category/

        public IActionResult Index()
        {
            return View("NotFound");
        }

        /// <summary>
        /// Listings the entry items corresponding the specified category value.
        /// </summary>
        /// <param name="categoryValue">The category value.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public IActionResult Listing(string categoryValue, int page=1)
        {
            CommonData();

            var list = this.blogService.GetEntriesByCategory(categoryValue, page);
            if(!list.Any())
                return View("_Empty");

            ViewBag.Title = string.Format("Ronald Rosier.NET - {0} Listing (page {1})", categoryValue, page);
            return View("Listing",list);
        }

    }
}
