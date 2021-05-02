using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Model;
using Rosier.Blog.Services;
using Xunit;

namespace Rosier.Blog.Services.Extentions.Tests
{
    public class ConvertToViewModelTests
    {
        [Fact]
        public void BlogEntry_BasicConvertions()
        {
            var entry = new BlogEntry()
            {
                BlogEntryId = Guid.NewGuid(),
                Content = "<p>some content</p>",
                CreationDate = DateTimeOffset.Now,
                Title = "This is the title",
                TotalComments = 0
            };

            var entryView = entry.ConvertToViewModel();

            Assert.Equal(entry.Title, entryView.Title);
            Assert.Equal(entry.Content, entryView.Content);
            Assert.Equal(entry.BlogEntryId.ToString(), entryView.ID);
            Assert.Equal(entry.TotalComments, entryView.NrComments);
            Assert.True(entryView.ShowTitleLink);
        }

        [Fact]
        public void BlogEntry_TimeString_No_Offset()
        {
            var date = new DateTimeOffset(2012, 1, 21, 14, 3, 46, new TimeSpan(0, 0, 0));
            var expected = "Sat, 21 Jan 2012 14:03:46 GMT";

            var entry = new BlogEntry()
            {
                CreationDate = date
            };

            var entryView = entry.ConvertToViewModel();

            Assert.Equal(expected, entryView.UtcTimeString);
        }

        [Fact]
        public void BlogEntry_TimeString_With_Positive_Offset()
        {
            var date = new DateTimeOffset(2012, 1, 21, 14, 3, 46, new TimeSpan(2, 0, 0));
            var expected = "Sat, 21 Jan 2012 12:03:46 GMT";

            var entry = new BlogEntry()
            {
                CreationDate = date
            };

            var entryView = entry.ConvertToViewModel();

            Assert.Equal(expected, entryView.UtcTimeString);
        }

        [Fact]
        public void BlogEntry_TimeString_With_Negative_Offset()
        {
            var date = new DateTimeOffset(2012, 1, 21, 14, 3, 46, new TimeSpan(-2, 0, 0));
            var expected = "Sat, 21 Jan 2012 16:03:46 GMT";

            var entry = new BlogEntry()
            {
                CreationDate = date
            };

            var entryView = entry.ConvertToViewModel();

            Assert.Equal(expected, entryView.UtcTimeString);
        }

        [Fact]
        public void BlogEntry_EntryLink()
        {
            var date = new DateTimeOffset(2012, 1, 21, 14, 3, 46, new TimeSpan(0, 0, 0));
            var strippedTitle = "this_is_some_title";
            var expected = "/Blog/2012/01/21/this_is_some_title";

            var entry = new BlogEntry()
            {
                CreationDate = date,
                StrippedDownTitle = strippedTitle
            };

            var entryView = entry.ConvertToViewModel();

            Assert.Equal(expected, entryView.EntryLink);
        }

        [Fact]
        public void BlogEntry_CategoryValue()
        {
            var expected = "aspnet_mvc";
            var entry = new BlogEntry()
            {
                Categories = new List<Category>()
                {
                    new Category(){
                        Name = "ASP.NET MVC",
                        Value = "aspnet_mvc"
                    }
                }
            };

            var entryView = entry.ConvertToViewModel();
            Assert.Equal(expected, entryView.Categories.First().Value);
        }
    }
}
