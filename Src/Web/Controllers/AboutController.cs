using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Rosier.Blog.Services;

namespace Rosier.Blog.Web.Controllers
{
    public class AboutController : BaseController
    {

        public AboutController(IBlogService service):base(service)
        {
            ViewBag.ActivePage = "About";
        }
        //
        // GET: /About/

        public IActionResult Index()
        {
            CommonData();
            return View("UnderConstruction");
        }

    }
}
