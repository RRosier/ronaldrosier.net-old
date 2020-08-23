using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Model;
using System.Data.Objects;

namespace Rosier.Blog.Data
{
    /// <summary>
    /// Entity Framework implementation for the blog collection repository.
    /// </summary>
    public class EntityBlogCollectionRepository : Repository<CollectionInfo>, IBlogCollectionRepository
    {
        public EntityBlogCollectionRepository(ObjectContext context):base(context){}

        /// <summary>
        /// Gets the collections info.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CollectionInfo> GetCollectionsInfo()
        {
            return this.objectSet;
        }

        /// <summary>
        /// Gets the collection by unique id.
        /// </summary>
        /// <param name="id">The unique id.</param>
        /// <returns></returns>
        public override CollectionInfo GetById(string id)
        {
            return this.objectSet.SingleOrDefault(c => c.Name == id);
        }
    }
}
