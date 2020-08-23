using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Rosier.Blog.Service.ViewModel;
using Rosier.Blog.Model;
using System.Globalization;
using System.Security.Cryptography;

namespace Rosier.Blog.Services
{
    /// <summary>
    /// Implementation of the syndication service.
    /// </summary>
    public class BlogService : IBlogService
    {
        private const int MaxEntries = 10;

        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogService"/> class.
        /// </summary>
        /// <param name="uow">The <see cref="IUnitOfWork"/> implementation.</param>
        public BlogService(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("uow");

            this.unitOfWork = uow;
        }

        /// <summary>
        /// Gets the latest blog entries.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BlogItemViewModel> GetAllEntries(int page)
        {
            var list = new List<BlogItemViewModel>();

            var totalentries = this.unitOfWork.Entries.TotalEntries;
            var toSkip = (page * MaxEntries) - MaxEntries;

            foreach (var entry in this.unitOfWork.Entries.GetEntries(toSkip, MaxEntries))
            {
                var viewEntry = entry.ConvertToViewModel();
                list.Add(viewEntry);
            }

            return list;
        }

        /// <summary>
        /// Gets the entries by year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public IEnumerable<BlogItemViewModel> GetEntriesByYear(int year, int page)
        {
            var list = new List<BlogItemViewModel>();

            var totalentries = this.unitOfWork.Entries.TotalEntries;
            var toSkip = (page * MaxEntries) - MaxEntries;

            var entries = (from e in this.unitOfWork.Entries.GetAll()
                           where e.CreationDate.Year == year
                           select e).Skip(toSkip).Take(MaxEntries);

            foreach (var entry in entries)
            {
                var viewEntry = entry.ConvertToViewModel();
                list.Add(viewEntry);
            }

            return list;
        }

        /// <summary>
        /// Gets the entries by month.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public IEnumerable<BlogItemViewModel> GetEntriesByMonth(int year, int month, int page)
        {
            var list = new List<BlogItemViewModel>();

            var totalentries = this.unitOfWork.Entries.TotalEntries;
            var toSkip = (page * MaxEntries) - MaxEntries;

            var entries = (from e in this.unitOfWork.Entries.GetAll()
                           where e.CreationDate.Year == year
                            && e.CreationDate.Month == month
                           select e).Skip(toSkip).Take(MaxEntries);

            foreach (var entry in entries)
            {
                var viewEntry = entry.ConvertToViewModel();
                list.Add(viewEntry);
            }

            return list;
        }

        /// <summary>
        /// Gets the entries by day.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        public IEnumerable<BlogItemViewModel> GetEntriesByDay(int year, int month, int day, int page)
        {
            var list = new List<BlogItemViewModel>();

            var totalentries = this.unitOfWork.Entries.TotalEntries;
            var toSkip = (page * MaxEntries) - MaxEntries;

            var entries = (from e in this.unitOfWork.Entries.GetAll()
                           where e.CreationDate.Year == year
                            && e.CreationDate.Month == month
                            && e.CreationDate.Day == day
                           select e).Skip(toSkip).Take(MaxEntries);

            foreach (var entry in entries)
            {
                var viewEntry = entry.ConvertToViewModel();
                list.Add(viewEntry);
            }

            return list;
        }

        /// <summary>
        /// Gets the entries by category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        public IEnumerable<BlogItemViewModel> GetEntriesByCategory(string category, int page)
        {
            var list = new List<BlogItemViewModel>();

            var totalentries = this.unitOfWork.Entries.TotalEntries;
            var toSkip = (page * MaxEntries) - MaxEntries;

            var entries = (from e in this.unitOfWork.Entries.GetAll()
                           where e.Categories.Where(c => c.Value.Equals(category)).Count() > 0
                           select e).Skip(toSkip).Take(MaxEntries);

            foreach (var entry in entries)
            {
                var viewEntry = entry.ConvertToViewModel();

                list.Add(viewEntry);
            }

            return list;
        }

        /// <summary>
        /// Gets the entry.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public BlogItemViewModel GetEntry(int year, int month, int day, string title)
        {
            var entries = from e in this.unitOfWork.Entries.GetAll()
                          where e.CreationDate.Year == year
                              && e.CreationDate.Month == month
                              && e.CreationDate.Day == day
                              && e.StrippedDownTitle == title
                          select e;

            if (entries.Count() != 1)
                return null;

            var entry = entries.First();

            var viewEntry = entry.ConvertToViewModel();
            viewEntry.ShowTitleLink = false;

            foreach (var c in entry.Comments.OrderBy(c => c.CreationDate))
            {
                //commentVO = c.ConvertToViewModel();

                var commentVO = new CommentDisplayViewModel();
                commentVO.Content = c.Content;
                commentVO.DisplayName = c.Owner.DisplayName;
                commentVO.EmailHash = c.Owner.EmailHash;
                commentVO.UtcTimeString = c.CreationDate.UtcDateTime.ToString("MM/dd/yyyy HH:mm");
                commentVO.IsBlogOwner = c.IsEntryOwner;
                commentVO.Website = c.Owner.HomePage;
                viewEntry.Comments.Add(commentVO);
            }

            return viewEntry;
        }

        /// <summary>
        /// Gets the archive list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ArchiveViewModel> GetArchiveList()
        {
            var l = from e in this.unitOfWork.Entries.GetAll()
                    group e by new { MonthString = e.CreationDate.ToString("MMMM", CultureInfo.CreateSpecificCulture("en-us")), e.CreationDate.Month, e.CreationDate.Year } into g
                    orderby g.Key.Year descending, g.Key.Month descending
                    select new ArchiveViewModel
                    {
                        NrEntries = g.Count(),
                        MonthString = g.Key.MonthString,
                        Year = g.Key.Year,
                        Month = g.Key.Month
                    };

            return l;
        }

        /// <summary>
        /// Gets the tag cloud.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TagCloudViewModel> GetTagCloud()
        {
            var totalEntries = this.unitOfWork.Entries.TotalEntries;

            var list = this.unitOfWork.Categories.GetAllInclude().ToList();
            var maxValue = list.Count == 0 ? 0 : (decimal)list.Max(c => c.Entries.Count);
            var minValue = list.Count == 0 ? 0 : (decimal)list.Min(c => c.Entries.Count);

            var devideBy = maxValue - minValue == 0 ? 1 : maxValue - minValue;

            return (from c in list
                    orderby c.Name
                    select new TagCloudViewModel
                    {
                        Name = c.Name,
                        Value = c.Value,
                        CountOfCategories = c.Entries.Count(),
                        TotalEntries = totalEntries,
                        Weight = ((decimal)c.Entries.Count - minValue) / (devideBy)
                    });
        }

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public CommentDisplayViewModel AddComment(CommentEditViewModel comment)
        {
            var entry = this.unitOfWork.Entries.GetById(comment.EntryID);
            if (entry == null)
                return null;

            var commentID = Guid.NewGuid();
            var homePage = !string.IsNullOrWhiteSpace(comment.Website) ? comment.Website.Replace("http://", "") : string.Empty;
            Person owner = null;
            if (!string.IsNullOrWhiteSpace(comment.Email))
            {
                var emailHash = GetMd5Hash(comment.Email);
                owner = this.unitOfWork.People.GetByEmailHash(emailHash);
            }
            // if no email is provided, or the user with the email is not yet in the table
            if (owner == null)
            {   // create a new person for this comment
                owner = new Person
                {
                    DisplayName = comment.DisplayName,
                    EmailHash = string.Empty,
                    HomePage = homePage
                };
            }

            var isEntryOwner = owner == entry.Author;

            var c = new Comment()
            {
                CommentId = commentID,
                Content = comment.Content,
                CreationDate = DateTimeOffset.UtcNow,
                Owner = owner,
                IsEntryOwner = isEntryOwner
            };

            entry.Comments.Add(c);
            entry.TotalComments++;

            this.unitOfWork.Commit();

            var commentVO = new CommentDisplayViewModel();
            commentVO.Content = c.Content;
            commentVO.DisplayName = c.Owner.DisplayName;
            commentVO.EmailHash = c.Owner.EmailHash;
            commentVO.UtcTimeString = c.CreationDate.UtcDateTime.ToString("MM/dd/yyyy HH:mm");
            commentVO.IsBlogOwner = c.IsEntryOwner;
            commentVO.Website = c.Owner.HomePage;
            return commentVO;
        }

        /// <summary>
        /// Creates the MD5 hash for the input string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        private string GetMd5Hash(string input)
        {
            string hash = string.Empty;
            using (MD5 md5hash = MD5.Create())
            {

                byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }

                hash = sb.ToString();
            }
            return hash;
        }

    }
}
