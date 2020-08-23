using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Blog.Model
{
    /// <summary>
    /// Repository for managing people.
    /// </summary>
    public interface IPeopleRepository : IRepository<Person>
    {
        /// <summary>
        /// Gets a user by email hash.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        Person GetByEmailHash(string hash);
    }
}
