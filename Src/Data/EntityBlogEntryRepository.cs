using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Model;
using System.Data.Objects;

namespace Rosier.Blog.Data
{
    /// <summary>
    /// Entity Framework implementation for the Blog Entry Repository.
    /// </summary>
    public class EntityBlogEntryRepository : Repository<BlogEntry>, IBlogEntryRepository
    {
        public EntityBlogEntryRepository(ObjectContext context):base(context) { }

        /// <summary>
        /// Gets the by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override BlogEntry GetById(string id)
        {
            Guid g;
            if(!Guid.TryParse(id, out g))
                throw new ArgumentException(string.Format("The parameter '{0}' is not a valid GUID.",id),"id");

            return this.objectSet.SingleOrDefault(e => e.BlogEntryId == g);
        }

        /// <summary>
        /// Gets the total count of entries.
        /// </summary>
        public int TotalEntries
        {
            get { return this.objectSet.Count(); }
        }

        /// <summary>
        /// Gets all entries.
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<BlogEntry> GetAll()
        {
            return this.objectSet.Include("Categories").OrderByDescending(e => e.CreationDate);
        }

        /// <summary>
        /// Gets a list of entries.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="maxEntries">The max entries.</param>
        /// <returns></returns>
        public IEnumerable<BlogEntry> GetEntries(int startIndex, int maxEntries)
        {
            return this.objectSet
                .Include("Categories")
                .Include("Author")
                .OrderByDescending(e => e.CreationDate)
                .Skip(startIndex)
                .Take(maxEntries);
        }


    }
}
