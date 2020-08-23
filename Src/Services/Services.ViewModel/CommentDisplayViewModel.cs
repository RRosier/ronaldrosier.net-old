using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Blog.Service.ViewModel
{
    /// <summary>
    /// View model for displaying comment information.
    /// </summary>
    public class CommentDisplayViewModel
    {
        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }
        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public string Website { get; set; }
        /// <summary>
        /// Gets or sets the UTC time string.
        /// </summary>
        /// <value>
        /// The UTC time string.
        /// </value>
        public string UtcTimeString { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is blog owner.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is blog owner; otherwise, <c>false</c>.
        /// </value>
        public bool IsBlogOwner { get; set; }
        /// <summary>
        /// Gets or sets the email hash.
        /// </summary>
        /// <value>
        /// The email hash.
        /// </value>
        public string EmailHash { get; set; }
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether adding the comment is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets a value indicating whether [show web site link].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show web site link]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowWebSiteLink
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Website);
            }
        }

    }
}
