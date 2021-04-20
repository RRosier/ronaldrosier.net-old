using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Model;
using System.Data.Objects;

namespace Rosier.Blog.Data
{
    /// <summary>
    /// Entity implementation of the Unit of Work pattern.
    /// </summary>
    public class EntityUnitOfWork : IUnitOfWork
    {
        private readonly ObjectContext context;
        private EntityBlogEntryRepository entryRepository;
        private EntityBlogCollectionRepository collectionRepository;
        private EntityBlogCategoryRepository categoryRepository;
        private EntityPeopleRespository peopleRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityUnitOfWork"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EntityUnitOfWork(ObjectContext context)
        {
            if (context == null)
                throw new ArgumentNullException("No context provided");

            this.context = context;
        }

        /// <summary>
        /// Gets the blog entries repository.
        /// </summary>
        public IBlogEntryRepository Entries
        {
            get 
            {
                if (this.entryRepository == null)
                    this.entryRepository = new EntityBlogEntryRepository(this.context);
                return this.entryRepository;
            }
        }

        /// <summary>
        /// Gets the collections repository.
        /// </summary>
        public IBlogCollectionRepository Collections
        {
            get 
            {
                if (this.collectionRepository == null)
                    this.collectionRepository = new EntityBlogCollectionRepository(this.context);
                return this.collectionRepository;
            }
        }

        /// <summary>
        /// Gets the categories repository.
        /// </summary>
        public IBlogCategoryRepository Categories
        {
            get
            {
                if (this.categoryRepository == null)
                    this.categoryRepository = new EntityBlogCategoryRepository(this.context);
                return this.categoryRepository;
            }
        }

        /// <summary>
        /// Gets the people repository.
        /// </summary>
        public IPeopleRepository People
        {
            get
            {
                if (this.peopleRepository == null)
                    this.peopleRepository = new EntityPeopleRespository(this.context);
                return this.peopleRepository;
            }
        }

        /// <summary>
        /// Commits the changes in this transaction.
        /// </summary>
        public void Commit()
        {
            this.context.SaveChanges();
        }
    }
}
