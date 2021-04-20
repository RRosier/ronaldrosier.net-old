using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Rosier.Blog.Services;
using Rosier.Blog.Models;

namespace Rosier.Blog.Web.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly IBlogService blogService;
        public IndexModel(IBlogService blogService)
        {
            this.blogService = blogService ?? throw new ArgumentNullException(nameof(blogService));
        }
        public async Task OnGetAsync(string title, CancellationToken token)
        {
            if (string.IsNullOrEmpty(title))
            {
                var entries = await this.blogService.GetRecentArticlesAsync(10, token);
                this.Articles = entries.ToArray();
            }
            else
            {
                var entry = await this.blogService.GetRecentArticlesAsync(1, token);
                this.Articles = entry.ToArray();
            }
        }

        public Article[] Articles { get; set; }
    }
}
