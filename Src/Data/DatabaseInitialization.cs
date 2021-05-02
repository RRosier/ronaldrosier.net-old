using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosier.Blog.Model;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace Rosier.Blog.Data
{
    public class DatabaseInitialization : CreateDatabaseIfNotExists<EntityDataContext>
    {
        protected override void Seed(EntityDataContext context)
        {
            var category = new Category() { Name = ".NET", Value = "DotNet" };
            var category2 = new Category() { Name = "jQuery", Value = "jQuery" };

            var collection = new CollectionInfo()
            {
                Name = "Blog"
            };

            var person = new Person()
            {
                DisplayName = "Ronald",
                EmailHash = "ronald@rosier.net"
            };

            //context.People.Add(person);

            var entry1 = new BlogEntry()
            {
                Title = "Entry title",
                CreationDate = DateTimeOffset.Now,
                Content = "<p>This is some content</p>",
                StrippedDownTitle = "entry_title",
                Author = person,
                Categories = { category, category2 },
                BlogEntryId = Guid.NewGuid(),
                TotalComments = 0
            };

            var entry2 = new BlogEntry()
            {
                Title = "Another Entry title",
                CreationDate = DateTimeOffset.Now,
                Content = "<p>This is some content</p>",
                StrippedDownTitle = "another_entry_title",
                Author = person,
                Categories = { category },
                BlogEntryId = Guid.NewGuid(),
                TotalComments = 0
            };

            context.BlogEntries.Add(entry1);
            context.BlogEntries.Add(entry2);

            //context.Categories.Add(category);
            //context.Categories.Add(category2);

            context.CollectionInfos.Add(collection);
            
        }
    }
}
