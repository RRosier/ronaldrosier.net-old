using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rosier.Blog.Model;

namespace Rosier.Blog.Services
{
    /// <summary>
    /// Business actions on blog entries.
    /// </summary>
    public interface IBlogService
    {
        /// <summary>
        /// Gets the most recent entries asynchronous.
        /// </summary>
        /// <param name="count">The count of entries to retrieve.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>List of most recent {count} entries.</returns>
        Task<IEnumerable<BlogEntry>> GetRecentEntriesAsync(int count, CancellationToken token = default);
    }
}
