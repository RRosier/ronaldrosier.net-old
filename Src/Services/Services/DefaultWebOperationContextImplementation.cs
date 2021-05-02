using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.ServiceModel.Web;
using System.Net;

namespace Rosier.Blog.Services
{
    /// <summary>
    /// Default implementation, using the Current WebOperationContext
    /// </summary>
    public class DefaultWebOperationContextImplementation : IWebOperationContext
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultWebOperationContextImplementation"/> class.
        /// </summary>
        public DefaultWebOperationContextImplementation()
        {
        }

        /// <summary>
        /// Gets the base URI.
        /// </summary>
        public Uri BaseUri
        {
            get
            {
                return new Uri("");
                //return WebOperationContext.Current.IncomingRequest.UriTemplateMatch.BaseUri;
            }
        }

        /// <summary>
        /// Gets or sets the contenttype header for the outgoing response.
        /// </summary>
        /// <value>
        /// The contenttype.
        /// </value>
        public string OutgoingContentType 
        {
            get { return ""; } // WebOperationContext.Current.OutgoingResponse.ContentType; }
            set { } // WebOperationContext.Current.OutgoingResponse.ContentType = value; } 
        }

        /// <summary>
        /// Gets or sets the type of the incoming request.
        /// </summary>
        /// <value>
        /// The contenttype.
        /// </value>
        public string IncomingContentType 
        {
            get { return ""; } // WebOperationContext.Current.IncomingRequest.ContentType; }
        }

        /// <summary>
        /// Gets the description from slug header.
        /// </summary>
        /// <returns></returns>
        public string GetDescriptionFromSlugHeader()
        {
            return "";// WebOperationContext.Current.IncomingRequest.Headers["Slug"];
        }

        /// <summary>
        /// Gets or sets the Outgoing response status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public HttpStatusCode OutgoingStatusCode 
        {
            get { return HttpStatusCode.NotFound; }// WebOperationContext.Current.OutgoingResponse.StatusCode; }
            set { } // WebOperationContext.Current.OutgoingResponse.StatusCode = value; }
        }

        /// <summary>
        /// Sets the status as created.
        /// </summary>
        /// <param name="location">The location.</param>
        public void SetStatusAsCreated(Uri location)
        {
            //WebOperationContext.Current.OutgoingResponse.SetStatusAsCreated(location);
        }


        /// <summary>
        /// Gets the description from the slugh header.
        /// </summary>
        /// <returns></returns>
        public string GetDescriptionFromSlughHeader()
        {
            return ""; // WebOperationContext.Current.IncomingRequest.Headers["Slug"];
        }
    }
}
