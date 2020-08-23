using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Model;
using System.Data.Objects;

namespace Rosier.Blog.Data
{
    /// <summary>
    /// People respository implementation for the Entity Framework.
    /// </summary>
    public class EntityPeopleRespository : Repository<Person>, IPeopleRepository
    {
        public EntityPeopleRespository(ObjectContext context):base(context)
        {}

        /// <summary>
        /// Gets the person by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public override Person GetById(string id)
        {
            return this.objectSet.SingleOrDefault(p => p.PersonId.ToString() == id);
        }

        /// <summary>
        /// Gets a person by email hash.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public Person GetByEmailHash(string hash)
        {
            return this.objectSet.SingleOrDefault(p => p.EmailHash.Equals(hash));
        }
    }
}
