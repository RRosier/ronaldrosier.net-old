using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Model;
using System.Data.Objects;

namespace Rosier.Blog.Data
{
    public class EntityBlogCategoryRepository : Repository<Category>, IBlogCategoryRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityBlogCategoryRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EntityBlogCategoryRepository(ObjectContext context):base(context){}

        /// <summary>
        /// Gets category by unique id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        /// <remarks>
        /// The 'Value' is here concidered as the unique id. This makes it easier to find a specific category.
        /// </remarks>
        public override Category GetById(string id)
        {
            return this.objectSet.SingleOrDefault(c => c.Value == id);
        }

        /// <summary>
        /// Gets all include list of entries.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> GetAllInclude()
        {
            return this.objectSet.Include("Entries");
        }
    }
}
