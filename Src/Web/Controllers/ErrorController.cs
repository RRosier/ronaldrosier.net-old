using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rosier.Blog.Services;

namespace Rosier.Blog.Web.Controllers
{
    public class ErrorController : BaseController
    {

        public ErrorController(IBlogService service):base(service){}
        //
        // GET: /Error/

        public ActionResult Index()
        {
            return NotFound();
        }

        public ActionResult NotFound()
        {
            CommonData();
            return View();
        }

    }
}
