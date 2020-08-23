using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public ActionResult Index()
        {
            CommonData();
            return View("UnderConstruction");
        }

    }
}
