using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Syndication;
using Rosier.Blog.Model;
using System.IO;
using System.Xml;
using Rosier.Blog.Service.ViewModel;

namespace Rosier.Blog.Services
{
    /// <summary>
    /// Extentions to facilitate conversions between Model objects and Syndication objects.
    /// </summary>
    public static class Extentions
    {
        #region Syndication Extentions

        /// <summary>
        /// Converts a SyndicationItem to a representing blog entry.
        /// </summary>
        /// <param name="item">The syndication item.</param>
        /// <returns>A <see cref="BlogEntry"/> representation of the <see cref="SyndicationItem"/> object.</returns>
        public static BlogEntry ConvertToModelEntry(this SyndicationItem item)
        {
            var blogEntry = new BlogEntry();

            blogEntry.Title = item.Title.Text;

            // assume the content is always a plain (html) string.
            blogEntry.Content = ((TextSyndicationContent)item.Content).Text;

            //Stream s = new MemoryStream();
            //XmlWriter writer = XmlWriter.Create(s);
            //item.Content.WriteTo(writer, "Content", string.Empty);
            //writer.Flush();
            //writer.Close();
            //s.Position = 0;
            //XmlReader reader = XmlReader.Create(s);
            //blogEntry.Content = reader.ReadContentAsString();

            blogEntry.CreationDate = item.PublishDate;
            blogEntry.LastUpdateDate = item.LastUpdatedTime;

            return blogEntry;
        }

        /// <summary>
        /// Converts the Model BlogEntry to a syndication item.
        /// </summary>
        /// <param name="entry">The model blog entry.</param>
        /// <returns>A <see cref="SyndicationItem"/> representation of the <see cref="BlogEntry"/> object.</returns>
        public static SyndicationItem ConvertToSyndicationItem(this BlogEntry entry, Uri baseUri)
        {
            SyndicationItem sItem = new SyndicationItem();
            sItem.Id = entry.BlogEntryId.ToString();
            sItem.PublishDate = entry.CreationDate;
            sItem.LastUpdatedTime = entry.LastUpdateDate;
            sItem.Title = new TextSyndicationContent(entry.Title);
            // assume only one author - Me
            if (entry.Author != null)
                sItem.Authors.Add(entry.Author.ConvertToSyndicationPerson());
            sItem.BaseUri = baseUri;
            sItem.Content = new TextSyndicationContent(entry.Content);

            sItem.Links.Add(new SyndicationLink(new Uri(entry.CreateBlogUrl(baseUri.Authority))));

            foreach (var category in entry.Categories)
                sItem.Categories.Add(category.ConvertToSyndicationCategory());

            return sItem;
        }

        /// <summary>
        /// Converts the Model Person to a syndication person.
        /// </summary>
        /// <param name="person">The model person.</param>
        /// <returns></returns>
        public static SyndicationPerson ConvertToSyndicationPerson(this Person person)
        {
            SyndicationPerson sPerson = new SyndicationPerson();
            sPerson.Name = person.DisplayName;
            sPerson.Email = person.EmailHash;

            return sPerson;
        }

        /// <summary>
        /// Converts to Model category to a syndication category.
        /// </summary>
        /// <param name="category">The model category.</param>
        /// <returns></returns>
        public static SyndicationCategory ConvertToSyndicationCategory(this Category category)
        {
            SyndicationCategory sCategory = new SyndicationCategory();
            sCategory.Label = category.Name;
            sCategory.Name = category.Value;

            return sCategory;
        }

        #endregion

        #region ViewModel extentions

        public static BlogItemViewModel ConvertToViewModel(this BlogEntry entry)
        {
            var viewEntry = new BlogItemViewModel()
            {
                ID = entry.BlogEntryId.ToString(),
                Title = entry.Title,
                UtcTimeString = entry.CreationDate.ToString("r"),
                NrComments = entry.TotalComments,
                Content = entry.Content,
                ShowTitleLink = true,
                AllowAddComments = entry.LastUpdateDate.AddMonths(2) > DateTimeOffset.Now,
                EntryLink = string.Format("/Blog/{0}/{1}/{2}/{3}",
                    entry.CreationDate.Year,
                    entry.CreationDate.Month.ToString("00"),
                    entry.CreationDate.Day.ToString("00"),
                    entry.StrippedDownTitle)
            };

            foreach (var cat in entry.Categories)
            {
                viewEntry.Categories.Add(new CategoryViewModel() { Name = cat.Name, Value = cat.Value });
            }

            return viewEntry;
        }

        #endregion
    }
}
