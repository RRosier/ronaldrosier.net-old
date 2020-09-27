using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Rosier.Blog.Model;

namespace Rosier.Blog.Services
{
    public class BlogService : IBlogService
    {
        private IEnumerable<BlogEntry> TestEntries =>
            new List<BlogEntry>
            {
                new BlogEntry{ID=1, Title="First Blog Entry", CreationDate=DateTime.UtcNow.AddDays(5), LastUpdateDate=DateTime.UtcNow.AddDays(5), Author="Ronald Rosier", Content="This is the blog content"},
                new BlogEntry{ID=2, Title="Second Blog Entry", CreationDate=DateTime.UtcNow.AddDays(4), LastUpdateDate=DateTime.UtcNow.AddDays(4), Author="Ronald Rosier", Content="This is the blog content"},
                new BlogEntry{ID=3, Title="Third Blog Entry", CreationDate=DateTime.UtcNow.AddDays(2), LastUpdateDate=DateTime.UtcNow.AddDays(2), Author="Ronald Rosier", Content="This is the blog content"},
            };

        public Task<IEnumerable<BlogEntry>> GetRecentEntriesAsync(int count, CancellationToken token = default)
        {
            return Task.FromResult(this.TestEntries.OrderBy(e => e.LastUpdateDate).AsEnumerable());
        }
    }
}
