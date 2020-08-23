using Rosier.Blog.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosier.Blog.Services
{
    public interface ISpamService
    {
        /// <summary>
        /// Verifies if the comment is spam or not.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <param name="ipAddress">The ip address.</param>
        /// <param name="userAgent">The user agent.</param>
        /// <returns>
        ///   <c>true</c> if the comment is spam, else <c>false</c>
        /// </returns>
        Task<bool> VerifySpamAsync(CommentEditViewModel comment, string ipAddress, string userAgent);

        /// <summary>
        /// Verifies the key provided.
        /// </summary>
        /// <returns></returns>
        Task VerifyKeyAsync();
    }
}
