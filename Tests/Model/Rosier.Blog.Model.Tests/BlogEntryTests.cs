using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Model;
using NUnit.Framework;

namespace Rosier.Blog.Model.Tests
{
    [TestFixture]
    public class BlogEntryTests
    {
        [TestCase("This is my string", "this_is_my_string")]
        [TestCase("CSS", "css")]
        [TestCase("ASP.NET MVC", "aspnet_mvc")]
        [TestCase(".NET", "net")]
        public void StripeDownTitle(string original, string expected)
        {
            string returned = Rosier.Blog.Model.BlogEntry.StripeDownTitle(original);
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void PrepareNewEntry_NoCreationDate()
        {
            const string expectedStrippedDownTitle = "this_is_my_title_just_for_test";
            var expectedCreationDate = DateTimeOffset.UtcNow.ToString("YYYYMMDDHHmm");

            var entry = new BlogEntry
                            {CreationDate = DateTimeOffset.MinValue, 
                                Title = "This is my title: just for test!"};

            entry.PrepareNewEntry();
            var creationDateToString = entry.CreationDate.ToString("YYYYMMDDHHmm");

            Assert.AreEqual(expectedStrippedDownTitle, entry.StrippedDownTitle);
            Assert.AreEqual(expectedCreationDate, creationDateToString);
        }

        [Test]
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

            Assert.AreEqual(expectedStrippedDownTitle, entry.StrippedDownTitle);
            Assert.AreEqual(expectedCreationDate, entry.CreationDate);
        }
    }

}
