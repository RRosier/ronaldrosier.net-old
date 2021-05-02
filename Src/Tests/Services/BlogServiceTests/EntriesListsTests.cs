using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Service;
using Rosier.Blog.Model;
using Moq;
using Xunit;

namespace Rosier.Blog.Services.BlogServiceTests
{
    public class EntryListsTests: IDisposable
    {
        IBlogService service;
        Mock<IUnitOfWork> unitOfWork;

        public EntryListsTests()
        {
            this.unitOfWork = new Mock<IUnitOfWork>();
            this.service = new BlogService(this.unitOfWork.Object);
        }

        public void Dispose()
        {
            this.service = null;
            this.unitOfWork = null;
        }

        #region Entry List by Category

        [Fact]
        public void GetEntriesByCategory_Success()
        {
            var list = CreateList();

            this.unitOfWork.Setup(s => s.Entries.GetAll()).Returns(list);

            var category = "aspnetmvc";
            var filteredList = this.service.GetEntriesByCategory(category, 1);

            Assert.Single(filteredList);
        }

        [Fact]
        public void GetEntriesByCategory_NoEntriesFoundList_ReturnEmptyList()
        {
            var list = CreateList();

            this.unitOfWork.Setup(s => s.Entries.GetAll()).Returns(list);

            var category = "unknowncategory";
            var filteredList = this.service.GetEntriesByCategory(category, 1);

            Assert.Empty(filteredList);
        }

        [Fact]
        public void GetEntriesByCategory_NullAsSearchedCategory_ReturnsEmptyList()
        {
            var list = CreateList();

            this.unitOfWork.Setup(s => s.Entries.GetAll()).Returns(list);

            string category = null;
            var filteredList = this.service.GetEntriesByCategory(category, 1);

            Assert.Empty(filteredList);

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
