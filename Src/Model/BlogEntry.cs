using System;
using System.Collections.Generic;
using System.Text;

namespace Rosier.Blog.Model
{
    /// <summary>
    /// Represents an Entry in the blog.
    /// </summary>
    public class BlogEntry
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int ID { get; set; }

        public Guid BlogEntryId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public Person Author { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTimeOffset CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        public DateTimeOffset LastUpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public string StrippedDownTitle { get; set; }

        public int TotalComments { get; set; }

        /// <summary>
        /// Initializes fields for a new entry.
        /// </summary>
        public void PrepareNewEntry()
        {
            this.BlogEntryId = Guid.NewGuid();
            this.TotalComments = 0;
            if (this.CreationDate == DateTimeOffset.MinValue)
                this.CreationDate = DateTimeOffset.UtcNow;
            this.StrippedDownTitle = StripeDownTitle(this.Title);
        }

        /// <summary>
        /// Creates the blog URL.
        /// </summary>
        /// <param name = "baseUrl" > The base URL.</param>
        /// <returns></returns>
        public string CreateBlogUrl(string baseUrl)
        {
            if (!baseUrl.EndsWith("/"))
                baseUrl = baseUrl + "/";

            var location = string.Format("http://{0}blog/{1}/{2}/{3}/{4}",
                            baseUrl,
                            this.CreationDate.ToString("yyyy"),
                            this.CreationDate.ToString("MM"),
                            this.CreationDate.ToString("dd"),
                            this.StrippedDownTitle);

            return location;
        }

        /// <summary>
        /// Strippes down a title.
        /// </summary>
        /// <param name = "title" > The title.</param>
        /// <returns></returns>
        public static string StripeDownTitle(string title)
        {
            var rTitle = title.Replace(' ', '_');
            StringBuilder sb = new StringBuilder();

            foreach (var c in title.Replace(' ', '_'))
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || (c == '_'))
                    sb.Append(c);
            }

            return sb.ToString().ToLower();
        }
    }
}
