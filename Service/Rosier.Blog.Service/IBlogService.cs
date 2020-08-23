using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Rosier.Blog.Service.ViewModel;
using Rosier.Blog.Model;
using System.ServiceModel.Syndication;

namespace Rosier.Blog.Services
{
    /// <summary>
    /// Interfaces describing the service used in the AtomPub Service.
    /// </summary>
    public interface IBlogService
    {
        /// <summary>
        /// Gets the archive list.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ArchiveViewModel> GetArchiveList();


        /// <summary>
        /// Gets all entries.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        IEnumerable<BlogItemViewModel> GetAllEntries(int page);

        /// <summary>
        /// Gets the tag cloud.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TagCloudViewModel> GetTagCloud();

        /// <summary>
        /// Gets the entry.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        BlogItemViewModel GetEntry(int year, int month, int day, string title);

        /// <summary>
        /// Gets the entries by year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        IEnumerable<BlogItemViewModel> GetEntriesByYear(int year, int page);

        /// <summary>
        /// Gets the entries by month.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        IEnumerable<BlogItemViewModel> GetEntriesByMonth(int year, int month, int page);

        /// <summary>
        /// Gets the entries by day.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="day">The day.</param>
        /// <param name="page">The page.</param>
        /// <returns></returns>
        IEnumerable<BlogItemViewModel> GetEntriesByDay(int year, int month, int day, int page);

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        CommentDisplayViewModel AddComment(CommentEditViewModel comment);

        /// <summary>
        /// Gets the entries by category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        IEnumerable<BlogItemViewModel> GetEntriesByCategory(string category, int page);
    }
}
