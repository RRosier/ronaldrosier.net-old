using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Rosier.Blog.Model;
using Rosier.Blog.Services;
using Rosier.Blog.Web.Controllers;
using Xunit;

namespace Rosier.Blog.Web
{
    public class BlogControllerTests
    {
        private readonly Mock<IBlogService> blogServiceMock = new Mock<IBlogService>();
        private readonly Mock<ILogger<BlogController>> loggerMock = new Mock<ILogger<BlogController>>();
        private readonly BlogController controller;

        public BlogControllerTests()
        {
            this.controller = new BlogController(this.blogServiceMock.Object, this.loggerMock.Object);
        }

        public static IEnumerable<object[]> InitializationData() =>
            new List<object[]> {
                new object[] { null, new Mock<ILogger<BlogController>>().Object }
            };

        [Theory]
        [MemberData(nameof(InitializationData))]
        public void InitializationException(IBlogService service, ILogger<BlogController> logger)
        {
            Assert.Throws<ArgumentNullException>(() => new BlogController(service, logger));
        }

        [Fact]
        public async Task IndexReturnsListView()
        {
            var result = await this.controller.Index(CancellationToken.None);
            var view = Assert.IsType<ViewResult>(result);
            Assert.Equal("List", view.ViewName);
        }

        [Fact]
        public async Task IndexReturnsListViewWithModel()
        {
            var entries = new[] { new BlogEntry(), new BlogEntry() };
            this.blogServiceMock.Setup(s => s.GetRecentEntriesAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(entries);

            var result = await this.controller.Index();
            var view = Assert.IsType<ViewResult>(result);
            Assert.Equal(entries, view.Model);
        }

        [Fact]
        public async Task IndexReturnsMostRecent10Entries()
        {
            var token = new CancellationTokenRegistration().Token;
            await this.controller.Index(token);

            this.blogServiceMock.Verify(s => s.GetRecentEntriesAsync(10, token), Times.Once());
        }
    }
}
