using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Rosier.Blog.Model
{
    /// <summary>
    /// Represents an Entry in the blog.
    /// </summary>
    public class BlogEntry
    {
        /// <summary>
        /// The title of the blog entry
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// The unique id of the blog entry
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public Guid BlogEntryId { get; set; }

        /// <summary>
        /// The content of the blog entry
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// The owner of the blog entry
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public virtual Person Author { get; set; }

        /// <summary>
        /// The creation date of the blog entry
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTimeOffset CreationDate { get; set; }

        /// <summary>
        /// The last update date of the blog entry
        /// </summary>
        /// <value>
        /// The last update date.
        /// </value>
        public DateTimeOffset LastUpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the total amount of comments.
        /// </summary>
        /// <value>
        /// The total amount comments.
        /// </value>
        public int TotalComments { get; set; }

        public string StrippedDownTitle { get; set; }


        private ICollection<Category> categories;
        /// <summary>
        /// List of the categories assosiated with the blog entry
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public virtual ICollection<Category> Categories 
        {
            get
            {
                if (this.categories == null)
                    this.categories = new Collection<Category>();
                return this.categories;
            }
            set
            {
                this.categories = value;
            }
        }

        public ICollection<Comment> comments;
        /// <summary>
        /// List of the comments assosiated with the blog entry
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public virtual ICollection<Comment> Comments 
        {
            get
            {
                if (this.comments == null)
                    this.comments = new Collection<Comment>();
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }

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
        /// <param name="baseUrl">The base URL.</param>
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
        /// <param name="title">The title.</param>
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
