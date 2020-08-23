using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Rosier.Blog.Service.ViewModel
{
    /// <summary>
    /// View model representing a blog item.
    /// </summary>
    public class BlogItemViewModel
    {

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public string ID { get; set; }
        /// <summary>
        /// Gets or sets the title of the item.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets or sets the UTC time string.
        /// </summary>
        /// <value>
        /// The UTC time string.
        /// </value>
        public string UtcTimeString { get; set; }
        /// <summary>
        /// Gets or sets the nr comments linked to the item.
        /// </summary>
        /// <value>
        /// The nr comments.
        /// </value>
        public int NrComments { get; set; }
        /// <summary>
        /// Gets or sets the content of the item.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether to show the title as a link to the entry.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show title link]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowTitleLink { get; set; }
        /// <summary>
        /// Gets or sets the entry link.
        /// </summary>
        /// <value>
        /// The entry link.
        /// </value>
        public string EntryLink { get; set; }

        /// <summary>
        /// Indicates if adding comments to this entry is allowed or not.
        /// </summary>
        /// <value>
        /// The allow add comments.
        /// </value>
        public bool AllowAddComments { get; set; }

        private List<CategoryViewModel> categories;
        /// <summary>
        /// Gets the categories.
        /// </summary>
        public ICollection<CategoryViewModel> Categories
        {
            get
            {
                if (this.categories == null)
                    this.categories = new List<CategoryViewModel>();
                return this.categories;
            }
        }

        private ICollection<CommentDisplayViewModel> comments;
        /// <summary>
        /// Gets the comments.
        /// </summary>
        public ICollection<CommentDisplayViewModel> Comments
        {
            get
            {
                if (this.comments == null)
                    this.comments = new Collection<CommentDisplayViewModel>();
                return this.comments;
            }
        }
    }
}
