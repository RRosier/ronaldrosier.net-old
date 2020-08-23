using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using Rosier.Blog.Model;

namespace Rosier.Blog.Data
{
    /// <summary>
    /// Entity Framework data context implementation.
    /// </summary>
    public class EntityDataContext: DbContext
    {

        /// <summary>
        /// Gets the object context entity data context.
        /// </summary>
        /// <value>
        /// The object context.
        /// </value>
        public ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }

        public DbSet<BlogEntry> BlogEntries { get; set; }
        public DbSet<CollectionInfo> CollectionInfos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Person> People { get; set; }
    }
}
