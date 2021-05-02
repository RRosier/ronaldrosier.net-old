using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using Rosier.Blog.Web;
using Rosier.Blog.Model;
using System.Collections.ObjectModel;
using Rosier.Blog.Services;
using System.ServiceModel.Syndication;
using System.IO;
using Xunit;

namespace Rosier.Blog.Service.AtomService.Tests
{
    public class AtomServiceTests
    {
        private AtomPubService atomService;
        private Mock<IUnitOfWork> unitOfWorkMock;
        private Mock<IWebOperationContext> webOperationContext;

        public AtomServiceTests()
        {
            this.unitOfWorkMock = new Mock<IUnitOfWork>();
            this.webOperationContext = new Mock<IWebOperationContext>();
            this.atomService = new AtomPubService(this.unitOfWorkMock.Object, this.webOperationContext.Object);
        }

        [Fact]
        public void Get_Service_Document()
        {
            //var list = CreateCollection();
            //this.serviceMock.Setup(s => s.GetResourceCollectionInfos()).Returns(list);
            //this.webOperationContext.Setup(woc => woc.BaseUri).Returns(new Uri("http://unittest.ronaldrosier.net/"));

            //var serviceDocument = this.atomService.GetDocument();
            //Assert.NotNull(serviceDocument);
            //Assert.AreEqual(1, serviceDocument.Document.Workspaces.Count, "Expected 1 workspace");
            //var workspace = serviceDocument.Document.Workspaces.First();
            //Assert.AreEqual(1, workspace.Collections.Count, "Expected 1 collection");
            //var collection = workspace.Collections.First();
            //var collectionCategories = collection.Categories.First() as InlineCategoriesDocument;
            //Assert.AreEqual(2, collectionCategories.Categories.Count, "Expected 2 categories");
        }

        #region AddEntry

        //[Test]
        //public void AddEntry_Success()
        //{
        //    this.unitOfWorkMock.Setup(s => s.Entries.Add(It.IsAny<BlogEntry>()));
        //    this.webOperationContext.Setup(s => s.IncomingContentType).Returns("application/atom+xml");

        //    Stream stream = CreateEntryStream();
        //    var collection = "blog";

        //    var formatter = this.atomService.AddEntry(collection, stream);
        //}

        #endregion

        #region Private Methods

        private Stream CreateEntryStream()
        {
            var str = @"<entry xmlns='http://www.w3.org/2005/Atom'>
                            <title>Blogapps supports Atom</title>
                                <link rel='alternate'
                                href=http://unittest.ronaldrosier.net/blog/D89AD0F5-44B6-4309-908D-32A78F038BC2 />
                                <link rel='edit' href='http://unittest.ronaldrosier.net/blog/D89AD0F5-44B6-4309-908D-32A78F038BC2' />
                            <category term='/UnitTest' />
                        <author>
                            <name>Ronald</name>
                            <email>ronald@ronaldrosier.net</email>
                        </author>
                        <id>D89AD0F5-44B6-4309-908D-32A78F038BC2</id>
                        <updated>2011-12-06T20:35:02Z</updated>
                        <published>2011-12-06T20:35:02Z</published>
                        <content type='html'><div xmlns='http://www.w3.org/1999/xhtml'>
                            <p><i>This is some unit test content</i></p></div>
                        </content>
                        <app:control xmlns:app='http://purl.org/atom/app#'>
                            <app:draft>no</app:draft>
                        </app:control>
                        <atom-draft 
                            xmlns='http://rollerweblogger.org/namespaces/app'>9</atom-draft>
                    </entry>";

            byte[] buffer = new UnicodeEncoding().GetBytes(str);
            MemoryStream ms = new MemoryStream(buffer);
            return ms;
        }

        /// <summary>
        /// Creates the collection.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<CollectionInfo> CreateCollection()
        {
            List<CollectionInfo> list = new List<CollectionInfo>();
            var collection = new CollectionInfo();
            collection.Name = "TestCollection";
            list.Add(collection);

            return list.AsEnumerable();
        }

        #endregion

    }
}
