using System;
using System.Collections.Generic;

namespace Rosier.Blog.Models
{
	/// <summary>
	/// Represents a single article in the blog.
	/// </summary>
	public class Article
	{
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the html-encoded content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTimeOffset CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        public DateTimeOffset LastUpdateDate { get; set; }

		/// <summary>
		/// Gets or sets the published date.
		/// </summary>
		public DateTimeOffset PublishDate { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
	}
}