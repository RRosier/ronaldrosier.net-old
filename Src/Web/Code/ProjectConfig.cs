using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Rosier.Blog.Web
{
    public sealed class ProjectConfig : ConfigurationSection
    {
        static ProjectConfig instance = null;
        static readonly object padlock = new object();

        public ProjectConfig() { }

        /// <summary>
        /// get the only instancd of the Project configuration
        /// </summary>
        public static ProjectConfig Current
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        // load configuration
                        instance = (ProjectConfig)ConfigurationManager.GetSection("SiteConfiguration");
                    }
                    return instance;
                }
            }
        }

        [ConfigurationProperty("baseUrl", DefaultValue = "http://www.ronaldrosier.com/", IsRequired = true)]
        public string BaseUrl
        {
            get { return (string)this["baseUrl"]; }
            set { this["baseUrl"] = value; }
        }

        [ConfigurationProperty("maxEntries", DefaultValue = "10", IsRequired = true)]
        public int MaxEntries
        {
            get { return (int)this["maxEntries"]; }
            set { this["maxEntries"] = value; }
        }
    }
}
