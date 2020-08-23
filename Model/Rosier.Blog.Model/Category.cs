using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Rosier.Blog.Model
{
    /// <summary>
    /// Represents a category in the blog.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the unique ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public int CategoryId { get; set; }

        /// <summary>
        /// The display name of the category
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// The value of the category
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        private ICollection<BlogEntry> entries;
        /// <summary>
        /// Gets or sets the entries.
        /// </summary>
        /// <value>
        /// The entries.
        /// </value>
        public ICollection<BlogEntry> Entries
        {
            get
            {
                if (this.entries == null)
                    this.entries = new Collection<BlogEntry>();
                return this.entries;
            }
            set
            {
                this.entries = value;
            }
        }

    }
}
