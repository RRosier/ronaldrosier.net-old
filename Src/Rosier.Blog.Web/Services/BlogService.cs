using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Rosier.Blog.Models;

namespace Rosier.Blog.Services
{
    public class BlogService : IBlogService
    {
        private IEnumerable<Article> TestEntries =>
            new List<Article>
            {
                new Article{ID=1, Title="First Blog Entry", PermaLink="first-blog-entry", CreationDate=DateTime.UtcNow.AddDays(5), LastUpdateDate=DateTime.UtcNow.AddDays(5), Author="Ronald Rosier", Content="This is the blog content"},
                new Article{ID=2, Title="Second Blog Entry", PermaLink="second-blog-entry", CreationDate=DateTime.UtcNow.AddDays(4), LastUpdateDate=DateTime.UtcNow.AddDays(4), Author="Ronald Rosier", Content="This is the blog content"},
                new Article{ID=3, Title="Third Blog Entry", PermaLink="third-blog-entry", CreationDate=DateTime.UtcNow.AddDays(2), LastUpdateDate=DateTime.UtcNow.AddDays(2), Author="Ronald Rosier", Content="This is the blog content"},
            };

        public Task<IEnumerable<Article>> GetRecentArticlesAsync(int count, CancellationToken token = default)
        {
            return Task.FromResult(this.TestEntries.OrderBy(e => e.LastUpdateDate).Take(count).AsEnumerable());
        }
    }
}
