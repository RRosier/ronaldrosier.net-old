using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Rosier.Blog.Model;

namespace Rosier.Blog.Data
{
    /// <summary>
    /// Generic Base class for the repositories.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Repository<T> : IRepository<T>
        where T :class
    {
        protected ObjectSet<T> objectSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(ObjectContext context)
        {
            this.objectSet = context.CreateObjectSet<T>();
        }

        /// <summary>
        /// Gets the object by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public abstract T GetById(string id);

        /// <summary>
        /// Queries using the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public IEnumerable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return this.objectSet.Where(filter);
        }

        /// <summary>
        /// Gets all objects.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return this.objectSet;
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(T entity)
        {
            this.objectSet.AddObject(entity);
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Remove(T entity)
        {
            this.objectSet.DeleteObject(entity);
        }
    }
}
