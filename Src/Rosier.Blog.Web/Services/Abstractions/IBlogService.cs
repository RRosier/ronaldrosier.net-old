using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rosier.Blog.Models;

namespace Rosier.Blog.Services
{
    /// <summary>
    /// Business actions on blog articles.
    /// </summary>
    public interface IBlogService
    {
        /// <summary>
        /// Gets the most recent articles asynchronous.
        /// </summary>
        /// <param name="count">The count of articles to retrieve.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>List of most recent {count} articles.</returns>
        Task<IEnumerable<Article>> GetRecentArticlesAsync(int count, CancellationToken token = default);
    }
}