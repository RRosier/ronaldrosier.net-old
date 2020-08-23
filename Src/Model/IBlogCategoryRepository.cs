using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Blog.Model
{
    /// <summary>
    /// interface for the category repository.
    /// </summary>
    public interface IBlogCategoryRepository: IRepository<Category>
    {
        /// <summary>
        /// Gets all include list of entries.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Category> GetAllInclude();
    }
}
