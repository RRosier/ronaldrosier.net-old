using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rosier.Blog.Service.ViewModel
{
    public class TagCloudViewModel:CategoryViewModel
    {
        private const int fontSizeMin = 80;
        private const int fontSizeMax = 240;
        
        public int CountOfCategories { get; set; }
        public int TotalEntries { get; set; }

        public decimal Weight { get; set; }

        /// <summary>
        /// Gets the name of the css class.
        /// </summary>
        /// <value>
        /// The name of the css class.
        /// </value>
        public string ClassName
        {
            get
            {
                var result = (CountOfCategories * 100) / TotalEntries;

                if (result <= 1)
                    return "tag1";
                if (result <= 4)
                    return "tag2";
                if (result <= 8)
                    return "tag3";
                if (result <= 12)
                    return "tag4";
                if (result <= 18)
                    return "tag5";
                if (result <= 30)
                    return "tag6";
                return result <= 50 ? "tag7" : "";
            }
        }

        /// <summary>
        /// Gets the size of the font.
        /// </summary>
        /// <value>
        /// The size of the font.
        /// </value>
        public string FontSize
        {
            get
            {
                var size = fontSizeMin + Math.Round((fontSizeMax - fontSizeMin) * Weight);
                return size + "%";
            }
        }
    }
}
