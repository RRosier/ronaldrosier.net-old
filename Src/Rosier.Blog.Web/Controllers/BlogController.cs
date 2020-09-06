using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rosier.Blog.Services.Abstractions;

namespace Rosier.Blog.Web.Controllers
{
    public class BlogController : Controller
    {  
        private readonly IBlogService blogService;
        private readonly ILogger logger;

        public BlogController(IBlogService blogService, ILogger<BlogController> logger)
        {
            this.blogService = blogService ?? throw new ArgumentNullException(nameof(blogService));
            this.logger = logger;
        }
    }
}