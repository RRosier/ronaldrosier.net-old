using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using Rosier.Blog.Model;
using Rosier.Blog.Services;
using Xunit;

namespace Rosier.Blog.Services.Extentions.Tests
{
    public class ConvertToSyndicationItemTests
    {
        [Fact]
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

            Assert.NotNull(syndicationItem);
            Assert.Equal(entry.BlogEntryId.ToString(), syndicationItem.Id);
            Assert.Equal(entry.CreationDate, syndicationItem.PublishDate);
            Assert.Equal(entry.LastUpdateDate, syndicationItem.LastUpdatedTime);
            Assert.Equal(entry.Title, syndicationItem.Title.Text);
            Assert.Equal(entry.Content, ((TextSyndicationContent)syndicationItem.Content).Text);
            Assert.Single(syndicationItem.Authors);
        }

        [Fact]
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

            Assert.Equal(2, syndicationItem.Categories.Count);
        }

        [Fact]
        public void ConvertToSyndicationPerson_simple()
        {
            var person = new Person()
            {
                DisplayName = "Ronald Rosier",
                EmailHash = "ronald@ronaldrosier.com"
            };

            var syndicationPerson = person.ConvertToSyndicationPerson();

            Assert.Equal(person.DisplayName, syndicationPerson.Name);
            Assert.Equal(person.EmailHash, syndicationPerson.Email);
        }

        [Fact]
        public void ConvertToSyndicationCategory_Simple()
        {
            var category = new Category()
            {
                Name = "Cat 1",
                Value = "cat1"
            };

            var syndicationCategory = category.ConvertToSyndicationCategory();

            Assert.NotNull(syndicationCategory);
            Assert.Equal(category.Name, syndicationCategory.Label);
            Assert.Equal(category.Value, syndicationCategory.Name);
        }
    }
}
