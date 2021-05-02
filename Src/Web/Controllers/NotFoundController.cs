﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Rosier.Blog.Services;

namespace Rosier.Blog.Web.Controllers
{
    public class NotFoundController : BaseController
    {
        //
        // GET: /NotFound/

        public NotFoundController(IBlogService service):base(service){}

        public IActionResult Index()
        {
            CommonData();
            return View("NotFound");
        }

    }
}
