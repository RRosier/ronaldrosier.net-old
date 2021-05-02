using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using System.ComponentModel;
using Xunit;

namespace Rosier.Blog.Web.Tests
{
    public class RouteTests: IDisposable
    {
        private RouteCollection routes;

        public RouteTests()
        {
            routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);
        }

        public void Dispose()
        {
            routes = null;
        }

        [Fact]
        public void Default_Route_Blog_Index()
        {
            RouteTestHelper.AssertRoute(routes, "~/",
                new { Controller = "blog", Action = "index" });
        }

        [Fact]
        public void Entry_Route()
        {
            RouteTestHelper.AssertRoute(routes, "~/Blog/2012/01/01/some_blog_entry",
                new { Controller = "Blog", Action = "Entry", year = "2012", month = "01", day = "01", title = "some_blog_entry" });
        }

        [Fact]
        public void List_Entries_By_Day()
        {
            RouteTestHelper.AssertRoute(routes, "~/Blog/2012/01/01",
                new { Controller = "Blog", Action = "ListByDay", year = "2012", month = "01", day = "01" });
        }

        [Fact]
        public void List_Entries_By_Month()
        {
            RouteTestHelper.AssertRoute(routes, "~/Blog/2012/01",
                new { Controller = "Blog", Action = "ListByMonth", year = "2012", month = "01" });
        }

        [Fact]
        public void List_Entries_By_Year()
        {
            RouteTestHelper.AssertRoute(routes, "~/Blog/2012",
                new { Controller = "Blog", Action = "ListByYear", year = "2012" });
        }

        [Fact]
        public void Category_By_Id()
        {
            RouteTestHelper.AssertRoute(routes, "~/Category/jQuery",
                new { Controller = "Category", Action = "Listing", categoryValue = "jQuery" });
        }

        [Fact]
        public void Contact()
        {
            RouteTestHelper.AssertRoute(routes, "~/Contact",
                new { Controller = "Contact", Action = "Index" });
        }

        [Fact]
        public void About()
        {
            RouteTestHelper.AssertRoute(routes, "~/About",
                new { Controller = "About", Action = "Index" });
        }

        [Fact]
        public void Ignore_axd_files()
        {
            Assert.Throws<NullReferenceException>(() =>
                RouteTestHelper.AssertRoute(routes, "~/some.axd",
                    new { Controller = "", Action = "" }));
        }

        [Fact]
        public void Ignore_axd_files_with_something_behind()
        {
            Assert.Throws<NullReferenceException>(() => 
                RouteTestHelper.AssertRoute(routes, "~/some.axd/with?something.behind",
                    new { Controller = "", Action = "" }));
        }

        [Fact]
        public void Ignore_xml_files()
        {
            Assert.Throws<NullReferenceException>(() => RouteTestHelper.AssertRoute(routes, "~/wlwmanifest.xml", new { Controller = "", Action = "" }));
        }
    }

    /// <summary>
    /// Help extentions for testing the routes.
    /// </summary>
    /// <remarks>
    /// Based on code from Phil Haack
    /// http://haacked.com/archive/2007/12/17/testing-routes-in-asp.net-mvc.aspx
    /// </remarks>
    internal static class RouteTestHelper
    {
        public static void AssertRoute(RouteCollection routes, string url, object expectations)
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(c => c.Request.AppRelativeCurrentExecutionFilePath)
                .Returns(url);

            RouteData routeData = routes.GetRouteData(httpContextMock.Object);
            Assert.NotNull(routeData);

            foreach (PropertyValue property in GetProperties(expectations))
            {
                Assert.True(string.Equals(property.Value.ToString(),
                    routeData.Values[property.Name].ToString(),
                    StringComparison.OrdinalIgnoreCase),
                    string.Format("Expected '{0}', not '{1}' for '{2}'",
                    property.Value, routeData.Values[property.Name], property.Name));
            }
        }

        public static IEnumerable<PropertyValue> GetProperties(object o)
        {
            if (o != null)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(o);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(o);
                    if (val != null)
                    {
                        yield return new PropertyValue { Name = prop.Name, Value = val };
                    }
                }
            }
        }
    }

    internal sealed class PropertyValue
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

}
