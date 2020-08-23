using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Blog.Model
{
    /// <summary>
    /// Represents a comment in the blog application.
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public Guid CommentId { get; set; }

        /// <summary>
        /// Title of the comment
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Creation date of the comment
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTimeOffset CreationDate { get; set; }

        /// <summary>
        /// Owner of the comment
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public virtual Person Owner { get; set; }

        /// <summary>
        /// The content of the comment
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Unique id of the blog entry assosiated with the comment
        /// </summary>
        /// <value>
        /// The blog entry id.
        /// </value>
        public Guid BlogEntryId { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the comment's owner is the same as the entry owner.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is entry owner; otherwise, <c>false</c>.
        /// </value>
        public bool IsEntryOwner { get; set; }
    }
}
