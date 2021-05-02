//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.ServiceModel;
//using System.ServiceModel.Syndication;
//using System.ServiceModel.Activation;
//using Rosier.Blog.Services;
//using Rosier.Blog.Model;
//using Rosier.Blog.Data;
//using System.IO;
//using System.Web.Hosting;
//using System.ServiceModel.Web;

//namespace Rosier.Blog.Web
//{
//    /// <summary>
//    /// Provides an AtomPub service for the blog.
//    /// </summary>
//    /// <remarks>
//    /// This uses the ServiceBase implementation of the WCF REST Starter Kit.
//    /// http://www.asp.net/downloads/starter-kits/wcf-rest
//    /// </remarks>
//    /// TODO: Set IncludeExceptioinDetailInFaults to false in production
//    /// TODO: Set AspNetCompatibilityRequirementsMode.Required to configure logon security
//    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single)]
//    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
//    public class AtomPubService : AtomPubServiceBase, IAtomPubService
//    {
//        //IBlogService service;
//        readonly IUnitOfWork unitOfWork;

//        /// <summary>
//        /// Initializes a new instance of the <see cref="AtomPubService"/> class.
//        /// </summary>
//        public AtomPubService()
//            :base(new DefaultWebOperationContextImplementation())
//        {
//            unitOfWork = new EntityUnitOfWork(new EntityDataContext().ObjectContext);
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="AtomPubService"/> class.
//        /// </summary>
//        /// <param name="unitOfWork">The unit of work.</param>
//        /// <param name="woContext">The wo context.</param>
//        public AtomPubService(IUnitOfWork unitOfWork, IWebOperationContext woContext)
//            : base(woContext)
//        {
//            this.unitOfWork = unitOfWork;
//        }

//        /// <summary>
//        /// Add the Atom entry to the collection. Return its id and the actual entry that was added to the collection.
//        /// If the item could not be added return null.
//        /// </summary>
//        /// <param name="collection">collection name</param>
//        /// <param name="entry">entry to be added</param>
//        /// <param name="location">URI for the added entry</param>
//        /// <returns></returns>
//        protected override SyndicationItem AddEntry(string collection, SyndicationItem entry, out Uri location)
//        {
//            var blogEntry = entry.ConvertToModelEntry();

//            bool isAdded = false;

//            try
//            {
//                blogEntry.PrepareNewEntry();
//                foreach (var sCat in entry.Categories)
//                {
//                    // search if the category already exists.
//                    var category = unitOfWork.Categories.GetById(BlogEntry.StripeDownTitle(sCat.Label));
//                    if (category == null)
//                    {   // create a new category
//                        category = new Category() { Name = sCat.Label, Value = BlogEntry.StripeDownTitle(sCat.Label) };
//                    }

//                    blogEntry.Categories.Add(category);
//                }

//                // TODO: add author based on logged-in user.
//                var author = this.unitOfWork.People.GetByEmailHash("4cf6ef00ca33cc3f4010b8f0fdd980fe");
//                blogEntry.Author = author;

//                this.unitOfWork.Entries.Add(blogEntry);
//                this.unitOfWork.Commit();
//                isAdded = true;
//            }
//            catch (Exception)
//            {   // TODO: Catch only specific exceptions.
//                isAdded = false;
//            }

//            if (isAdded)    // successful add
//            {
//                entry = blogEntry.ConvertToSyndicationItem(this.webOperationContext.BaseUri);

//                ConfigureAtomEntry(collection, entry, blogEntry.BlogEntryId.ToString(), out location);
//                this.sEntry = entry;
//                return sEntry;
//            }
//            else
//            {
//                this.sEntry = null;
//                location = null;
//                return null;
//            }
//        }

//        protected override SyndicationItem AddMedia(string collection, System.IO.Stream stream, string contentType, string description, out Uri location)
//        {
//            byte[] buffer = new byte[1024];
//            int byteCount;

//            string id = Guid.NewGuid().ToString();
//            string apPath = HostingEnvironment.ApplicationPhysicalPath+@"Content\Images\"+id+".png";

//                FileStream fs = new FileStream(apPath, FileMode.Create);
//                do
//                {
//                    byteCount = stream.Read(buffer, 0, buffer.Length);
//                    fs.Write(buffer, 0, buffer.Length);
//                } while (byteCount > 0);

//                fs.Close();

//                var sItem = new SyndicationItem();
//                sItem.Id = id;

//                ConfigureMediaEntry(collection, sItem, id, contentType, out location);

//                return sItem;
//        }

//        protected override SyndicationFeed CreateFeed(string collection)
//        {
//            return new SyndicationFeed()
//            {
//                Title = new TextSyndicationContent("Ronald Rosier.NET"),
//                Id="http://www.ronaldrosier.net/feed",
//                Description= new TextSyndicationContent("(ASP).NET, C#, System Architecture, and more...")
//            };
//        }

//        /// <summary>
//        /// Delete the Atom entry with the specified id. Return false if no such entry exists.
//        /// This method should be idempotent.
//        /// </summary>
//        /// <param name="collection">collection name</param>
//        /// <param name="id">id of the entry</param>
//        /// <returns></returns>
//        protected override bool DeleteEntry(string collection, string id)
//        {
//            var entry = unitOfWork.Entries.GetById(id);
//            if (entry == null)
//                return false;

//            try
//            {
//                unitOfWork.Entries.Remove(entry);
//                unitOfWork.Commit();
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }

//        protected override bool DeleteMedia(string collection, string id)
//        {
//            throw new NotImplementedException();
//        }

//        /// <summary>
//        /// Return the content types of items that can be added to the collection.
//        /// </summary>
//        /// <param name="collection">collection name.</param>
//        /// <returns></returns>
//        /// <remarks>
//        /// by default, all content-types are allowed.
//        /// </remarks>
//        protected override IEnumerable<string> GetAllowedContentTypes(string collection)
//        {
//            return new string[] { "*/*" };
//        }

//        /// <summary>
//        /// Returns the items in the collection in the specified range.
//        /// </summary>
//        /// <param name="collection">collection name</param>
//        /// <param name="startIndex"></param>
//        /// <param name="maxEntries"></param>
//        /// <param name="hasMoreEntries"></param>
//        /// <returns></returns>
//        protected override IEnumerable<SyndicationItem> GetEntries(string collection, int startIndex, int maxEntries, out bool hasMoreEntries)
//        {
//            var list = new List<SyndicationItem>();

//            hasMoreEntries = (startIndex + maxEntries) < this.unitOfWork.Entries.TotalEntries;

//            foreach (var entry in this.unitOfWork.Entries.GetEntries(startIndex, maxEntries))
//            {
//                list.Add(entry.ConvertToSyndicationItem(this.webOperationContext.BaseUri));
//            }

//            return list;
//        }

//        private SyndicationItem sEntry;
//        /// <summary>
//        /// Gets the SyndicationItem corresponding to the id. Return null if it does not exist
//        /// </summary>
//        /// <param name="collection">collection name</param>
//        /// <param name="id">id of the entry</param>
//        /// <returns></returns>
//        protected override SyndicationItem GetEntry(string collection, string id)
//        {
//            if (this.sEntry == null)
//            {
//                var entry = unitOfWork.Entries.GetById(id);

//                if (entry == null)
//                    return null;

//                sEntry = entry.ConvertToSyndicationItem(this.webOperationContext.BaseUri);
//            }
//            return this.sEntry;
//        }

//        /// <summary>
//        /// Gets the SyndicationItem corresponding to the id. Return null if it does not exist.
//        /// Set the contentType of the media item.
//        /// </summary>
//        /// <param name="collection">collection name</param>
//        /// <param name="id">id of the entry</param>
//        /// <param name="contentType">content type of the item</param>
//        /// <returns></returns>
//        protected override System.IO.Stream GetMedia(string collection, string id, out string contentType)
//        {
//            string apPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + @"Content\Images\" + id + ".png";
//            if (File.Exists(apPath))
//            {
//                byte[] buffer = new byte[0];
//                contentType = "image/png";
                
//                var fs = new FileStream(apPath, FileMode.Open);
//                return fs;
//            }
//            else
//            {
//                contentType = string.Empty;
//                return null;
//            }
//        }

//        /// <summary>
//        /// Return the service document describing the collections hosted by the service.
//        /// </summary>
//        /// <returns></returns>
//        /// TODO: expect for the moment only 1 Workspace is present.
//        protected override ServiceDocument GetServiceDocument()
//        {
//            List<ResourceCollectionInfo> collections = new List<ResourceCollectionInfo>();

//            var categories = this.unitOfWork.Categories.GetAll().ToList();

//            foreach (var col in this.unitOfWork.Collections.GetCollectionsInfo())
//            {
//                var resourceCollection = new ResourceCollectionInfo(new TextSyndicationContent(col.Name), GetAllEntriesUri(col.Name),
//                    null, GetAllowedContentTypes(col.Name));

//                var inlineCategoriesDocument = new InlineCategoriesDocument();
//                foreach (var cat in categories)
//                {
//                    inlineCategoriesDocument.Categories.Add(new SyndicationCategory(cat.Value, "", cat.Name));
//                }

//                resourceCollection.Categories.Add(inlineCategoriesDocument);

//                collections.Add(resourceCollection);
//            }

//            Workspace workSpace = new Workspace("Ronald Rosier Blog Service", collections);

//            var serviceDocument = new ServiceDocument();
//            serviceDocument.Workspaces.Add(workSpace);

//            return serviceDocument;
//        }

//        protected override bool IsValidCollection(string collection)
//        {
//            return collection.Equals("blog", StringComparison.OrdinalIgnoreCase);
//        }

//        /// <summary>
//        /// Update the Atom entry specified by the id. If none exists, return null. Return the updated Atom entry. Return null if the entry does not exist.
//        /// This method must be idempotent.
//        /// </summary>
//        /// <param name="collection">collection name</param>
//        /// <param name="id">id of the entry</param>
//        /// <param name="entry">Entry to put</param>
//        /// <returns></returns>
//        protected override SyndicationItem PutEntry(string collection, string id, System.ServiceModel.Syndication.SyndicationItem entry)
//        {
//            var headers = WebOperationContext.Current.IncomingRequest.Headers;

//            var originalEntry = this.unitOfWork.Entries.GetById(id);
//            if (originalEntry == null)
//                return null;

//            var newEntry = entry.ConvertToModelEntry();
//            try
//            {
//                originalEntry.Title = newEntry.Title;
//                originalEntry.Content = newEntry.Content;
//                originalEntry.StrippedDownTitle = BlogEntry.StripeDownTitle(newEntry.Title);
//                originalEntry.LastUpdateDate = newEntry.LastUpdateDate;

//                originalEntry.Categories.Clear();
//                foreach (var sCat in entry.Categories)
//                {
//                    // search if the category already exists.
//                    var category = unitOfWork.Categories.GetById(BlogEntry.StripeDownTitle(sCat.Label));
//                    if (category == null)
//                    {   // create a new category
//                        category = new Category() { Name = sCat.Label, Value = BlogEntry.StripeDownTitle(sCat.Label) };
//                    }

//                    originalEntry.Categories.Add(category);
//                }

//                this.unitOfWork.Commit();

//                return originalEntry.ConvertToSyndicationItem(this.webOperationContext.BaseUri);
//            }
//            catch (Exception ex)
//            {
//                // TODO: add logging
//                return null;
//            }
//        }

//        protected override bool PutMedia(string collection, string id, System.IO.Stream stream, string contentType, string description)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
