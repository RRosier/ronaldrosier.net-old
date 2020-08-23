using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Blog.Model
{
    /// <summary>
    /// Represents a person who has a certain role in the blog.
    /// This can be the owner of a post or the writer of a comment.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        /// <value>
        /// The person id.
        /// </value>
        public int PersonId { get; set; }

        /// <summary>
        /// The name to be displayed
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// The email address of this person
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string EmailHash { get; set; }

        /// <summary>
        /// Gets or sets the home page.
        /// </summary>
        /// <value>
        /// The home page.
        /// </value>
        public string HomePage { get; set; }
    }
}
