using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Blog.Model
{
    public interface IBlogCollectionRepository
    {
        /// <summary>
        /// Gets the collections info.
        /// </summary>
        /// <returns></returns>
        IEnumerable<CollectionInfo> GetCollectionsInfo();

    }
}
