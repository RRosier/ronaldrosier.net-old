using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Rosier.Blog.Model
{
    /// <summary>
    /// interface defining the data layer actions.
    /// </summary>
    public interface IBlogEntryRepository : IRepository<BlogEntry>
    {
        /// <summary>
        /// Gets the total count of entries.
        /// </summary>
        int TotalEntries { get; }

        /// <summary>
        /// Gets a list of entries.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="maxEntries">The max entries.</param>
        /// <returns></returns>
        IEnumerable<BlogEntry> GetEntries(int startIndex, int maxEntries);
    }
}
