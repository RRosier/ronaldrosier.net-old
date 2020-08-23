using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Blog.Service.ViewModel
{
    /// <summary>
    /// 
    /// </summary>
    public class ArchiveViewModel
    {

        public string Display 
        { 
            get
            {
                return string.Format("{0} {1} ({2})", this.MonthString, this.Year, this.NrEntries);
            }
        }
        public string Url
        {
            get
            {
                return string.Format("{0}/{1}", this.Year.ToString(), this.Month.ToString("00"));
            }
        }
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public string MonthString { get; set; }
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }
        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public int Month { get; set; }
        /// <summary>
        /// Gets or sets the nr entries.
        /// </summary>
        /// <value>
        /// The nr entries.
        /// </value>
        public int NrEntries { get; set; }
    }
}
