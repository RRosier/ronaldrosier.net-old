using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Blog.Model
{
    /// <summary>
    /// Interface for the Unit of Work pattern.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the blog entries repository.
        /// </summary>
        IBlogEntryRepository Entries { get; }
        /// <summary>
        /// Gets the collections repository.
        /// </summary>
        IBlogCollectionRepository Collections { get; }
        /// <summary>
        /// Gets the categories repository.
        /// </summary>
        IBlogCategoryRepository Categories { get; }
        IPeopleRepository People { get; }

        /// <summary>
        /// Commits the changes in this transaction.
        /// </summary>
        void Commit();
    }
}
