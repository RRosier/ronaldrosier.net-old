using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rosier.Blog.Services;

namespace Rosier.Blog.Web.Controllers
{
    /// <summary>
    /// Controller that handles all blog-related views.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BlogController : Controller
    {
        private readonly ILogger logger;
        private readonly IBlogService blogService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogController"/> class.
        /// </summary>
        /// <param name="blogService">The blog service.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">blogService</exception>
        public BlogController(IBlogService blogService, ILogger<BlogController> logger)
        {
            this.blogService = blogService ?? throw new ArgumentNullException(nameof(blogService));
            this.logger = logger;
        }

        /// <summary>
        /// List the most recent blog entries.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>View that list the most recent entries.</returns>
        public async Task<IActionResult> Index(CancellationToken token = default)
        {
            var entries = await this.blogService.GetRecentEntriesAsync(10, token);
            return View("List", entries);
        }
    }
}