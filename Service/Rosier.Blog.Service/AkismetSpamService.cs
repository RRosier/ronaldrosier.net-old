using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Akismet.Net;
using Rosier.Blog.Service.ViewModel;
using System.Threading.Tasks;

namespace Rosier.Blog.Services
{
    /// <summary>
    /// Service to verify spam
    /// </summary>
    public class AkismetSpamService : ISpamService
    {
        Akismet.Net.Akismet akismet;
        /// <summary>
        /// Initializes a new instance of the <see cref="AkismetSpamService"/> class.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="baseUrl">The base URL.</param>
        /// <param name="appVersion">The application version.</param>
        public AkismetSpamService(string key, string baseUrl, string appVersion)
        {
            this.akismet = new Akismet.Net.Akismet(key, new Uri(baseUrl), appVersion);
        }

        public async Task VerifyKeyAsync()
        {
            var keyVerified = await this.akismet.VerifyKeyAsync();

            if (!keyVerified)
            {
                throw new ArgumentException("The provided Akismet key cannot be verified!");
            }
        }

        /// <summary>
        /// Verifies if the comment is spam or not.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns>
        ///   <c>true</c> if the comment is spam, else <c>false</c>
        /// </returns>
        public async Task<bool> VerifySpamAsync(CommentEditViewModel comment, string ipAddress, string userAgent)
        {
            var akismetComment = this.akismet.CreateComment();
            akismetComment.CommentAuthor = comment.DisplayName;
            akismetComment.CommentAuthorEmail = comment.Email;
            akismetComment.CommentAuthorUrl = comment.Website;
            akismetComment.CommentContent = comment.Content;
            akismetComment.CommentType = Akismet.Net.CommentTypes.Comment;

            akismetComment.Permalink = comment.Permalink;
            akismetComment.Referrer = string.Empty;
            akismetComment.UserAgent = userAgent;
            akismetComment.UserIp = ipAddress;

            var commentResult = await this.akismet.CheckCommentAsync(akismetComment);

            return commentResult == CommentCheck.Ham;
        }
    }
}
