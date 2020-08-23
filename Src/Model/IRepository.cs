using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Rosier.Blog.Model
{
    /// <summary>
    /// Generic interfaces for repositories
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
        where T:class
    {
        /// <summary>
        /// Gets the object by its unique ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        T GetById(string id);
        /// <summary>
        /// Queries with the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        IEnumerable<T> Query(Expression<Func<T, bool>> filter);
        /// <summary>
        /// Returns all objects.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Add(T entity);
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Remove(T entity);
    }
}
