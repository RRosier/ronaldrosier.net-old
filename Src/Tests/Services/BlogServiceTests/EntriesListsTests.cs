using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rosier.Blog.Service;
using Rosier.Blog.Model;
using Moq;

namespace Rosier.Blog.Services.BlogServiceTests
{
    [TestFixture]
    public class EntryListsTests
    {
        IBlogService service;
        Mock<IUnitOfWork> unitOfWork;

        [SetUp]
        public void SetUp()
        {
            this.unitOfWork = new Mock<IUnitOfWork>();
            this.service = new BlogService(this.unitOfWork.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this.service = null;
            this.unitOfWork = null;
        }

        #region Entry List by Category

        [Test]
        public void GetEntriesByCategory_Success()
        {
            var list = CreateList();

            this.unitOfWork.Setup(s => s.Entries.GetAll()).Returns(list);

            var category = "aspnetmvc";
            var filteredList = this.service.GetEntriesByCategory(category, 1);

            Assert.That(filteredList.Count() == 1, string.Format("Expected number of items '{0}' but was '{1}'!", 1, list.Count()));
        }

        [Test]
        public void GetEntriesByCategory_NoEntriesFoundList_ReturnEmptyList()
        {
            var list = CreateList();

            this.unitOfWork.Setup(s => s.Entries.GetAll()).Returns(list);

            var category = "unknowncategory";
            var filteredList = this.service.GetEntriesByCategory(category, 1);

            Assert.IsNotNull(filteredList);
            Assert.That(filteredList.Count() == 0, string.Format("Expected number of items '{0}' but was '{1}'!", 0, list.Count()));
        }

        [Test]
        public void GetEntriesByCategory_NullAsSearchedCategory_ReturnsEmptyList()
        {
            var list = CreateList();

            this.unitOfWork.Setup(s => s.Entries.GetAll()).Returns(list);

            string category = null;
            var filteredList = this.service.GetEntriesByCategory(category, 1);

            Assert.IsNotNull(filteredList);
            Assert.That(filteredList.Count() == 0, string.Format("Expected number of items '{0}' but was '{1}'!", 0, list.Count()));

        }

        #endregion

        #region Private Methods

        private IEnumerable<BlogEntry> CreateList()
        {
            var list = new List<BlogEntry>()
            {
                new BlogEntry(){
                    Categories = new List<Category>(){
                        new Category(){
                            Value = "aspnetmvc",
                            Name = "ASP.NET MVC"
                        }
                    }
                },
                new BlogEntry(){
                    Categories = new List<Category>(){
                        new Category(){
                            Value = "jQuery",
                            Name = "jQuery"
                        }
                    }
                }
            };
            return list;
        }

        #endregion
    }
}
