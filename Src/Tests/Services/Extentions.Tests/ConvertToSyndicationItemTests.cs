using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using NUnit.Framework;
using Rosier.Blog.Model;
using Rosier.Blog.Services;

namespace Rosier.Blog.Services.Extentions.Tests
{
    [TestFixture]
    public class ConvertToSyndicationItemTests
    {
        [Test]
        public void ConvertToSyndicationItem_SimpleEntry()
        {
            var entry = new BlogEntry()
            {
                BlogEntryId = Guid.NewGuid(),
                Title = "My unit test title",
                Content = "<p>this is some content</p>",
                CreationDate = DateTimeOffset.Now.AddDays(-2),
                LastUpdateDate = DateTimeOffset.Now,
                Author = new Person()
                {
                    DisplayName = "Ronald Rosier",
                    EmailHash = "ronald@ronaldrosier.com"
                }
            };

            var syndicationItem = entry.ConvertToSyndicationItem(new Uri("http://unittest.ronaldrosier.net"));

            Assert.IsNotNull(syndicationItem, "Expected an item");
            Assert.AreEqual(entry.BlogEntryId.ToString(), syndicationItem.Id);
            Assert.AreEqual(entry.CreationDate, syndicationItem.PublishDate);
            Assert.AreEqual(entry.LastUpdateDate, syndicationItem.LastUpdatedTime);
            Assert.AreEqual(entry.Title, syndicationItem.Title.Text);
            Assert.AreEqual(entry.Content, ((TextSyndicationContent)syndicationItem.Content).Text);
            Assert.AreEqual(1, syndicationItem.Authors.Count, "Expected only 1 author");
        }

        [Test]
        public void ConvertToSyndicationItem_With_Categories()
        {
            var entry = new BlogEntry()
            {
                BlogEntryId = Guid.NewGuid(),
                Title = "My unit test title",
                Content = "<p>this is some content</p>",
                CreationDate = DateTimeOffset.Now.AddDays(-2),
                LastUpdateDate = DateTimeOffset.Now,
                Author = new Person()
                {
                    DisplayName = "Ronald Rosier",
                    EmailHash = "ronald@ronaldrosier.com"
                },
                Categories =
                {
                    new Category(){ Name="Cat1", Value="cat1"},
                    new Category(){ Name="Cat2", Value="cat2"}
                }
            };

            var syndicationItem = entry.ConvertToSyndicationItem(new Uri("http://unittest.ronaldrosier.net"));

            Assert.AreEqual(2, syndicationItem.Categories.Count);
        }

        [Test]
        public void ConvertToSyndicationPerson_simple()
        {
            var person = new Person()
            {
                DisplayName = "Ronald Rosier",
                EmailHash = "ronald@ronaldrosier.com"
            };

            var syndicationPerson = person.ConvertToSyndicationPerson();

            Assert.AreEqual(person.DisplayName, syndicationPerson.Name);
            Assert.AreEqual(person.EmailHash, syndicationPerson.Email);
        }

        [Test]
        public void ConvertToSyndicationCategory_Simple()
        {
            var category = new Category()
            {
                Name = "Cat 1",
                Value = "cat1"
            };

            var syndicationCategory = category.ConvertToSyndicationCategory();

            Assert.IsNotNull(syndicationCategory);
            Assert.AreEqual(category.Name, syndicationCategory.Label);
            Assert.AreEqual(category.Value, syndicationCategory.Name);
        }
    }
}
