using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Model;
using Xunit;

namespace Rosier.Blog.Model.Tests
{
    public class BlogEntryTests
    {
        [Theory]
        [InlineData("This is my string", "this_is_my_string")]
        [InlineData("CSS", "css")]
        [InlineData("ASP.NET MVC", "aspnet_mvc")]
        [InlineData(".NET", "net")]
        public void StripeDownTitle(string original, string expected)
        {
            string returned = Rosier.Blog.Model.BlogEntry.StripeDownTitle(original);
            Assert.Equal(expected, returned);
        }

        [Fact]
        public void PrepareNewEntry_NoCreationDate()
        {
            const string expectedStrippedDownTitle = "this_is_my_title_just_for_test";
            var expectedCreationDate = DateTimeOffset.UtcNow.ToString("YYYYMMDDHHmm");

            var entry = new BlogEntry
                            {CreationDate = DateTimeOffset.MinValue, 
                                Title = "This is my title: just for test!"};

            entry.PrepareNewEntry();
            var creationDateToString = entry.CreationDate.ToString("YYYYMMDDHHmm");

            Assert.Equal(expectedStrippedDownTitle, entry.StrippedDownTitle);
            Assert.Equal(expectedCreationDate, creationDateToString);
        }

        [Fact]
        public void PrepareNewEntry_WithCreationDate()
        {
            const string expectedStrippedDownTitle = "this_is_my_title_just_for_test";
            var expectedCreationDate = DateTimeOffset.UtcNow;

            var entry = new BlogEntry
            {
                CreationDate = expectedCreationDate,
                Title = "This is my title: just for test!"
            };

            entry.PrepareNewEntry();

            Assert.Equal(expectedStrippedDownTitle, entry.StrippedDownTitle);
            Assert.Equal(expectedCreationDate, entry.CreationDate);
        }
    }

}
