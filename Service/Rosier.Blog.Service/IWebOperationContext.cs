using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Blog.Services
{
    /// <summary>
    /// Interface to abstract the WebOperationContext features in the atom pub service.
    /// </summary>
    public interface IWebOperationContext
    {
        /// <summary>
        /// Gets the base URI.
        /// </summary>
        Uri BaseUri { get; }
        /// <summary>
        /// Gets or sets the contenttype header for the outgoing response.
        /// </summary>
        /// <value>
        /// The contenttype.
        /// </value>
        string OutgoingContentType { get; set; }

        /// <summary>
        /// Gets or sets the type of the incoming request.
        /// </summary>
        /// <value>
        /// The contenttype.
        /// </value>
        string IncomingContentType { get; }

        /// <summary>
        /// Gets or sets the Outgoing response status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        System.Net.HttpStatusCode OutgoingStatusCode { get; set; }

        /// <summary>
        /// Gets the description from the slugh header.
        /// </summary>
        /// <returns></returns>
        string GetDescriptionFromSlughHeader();


        /// <summary>
        /// Sets the status as created.
        /// </summary>
        /// <param name="location">The location.</param>
        void SetStatusAsCreated(Uri location);

    }
}
