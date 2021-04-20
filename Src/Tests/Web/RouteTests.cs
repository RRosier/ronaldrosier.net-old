using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using System.ComponentModel;

namespace Rosier.Blog.Web.Tests
{
    [TestFixture]
    public class RouteTests
    {
        private RouteCollection routes;

        [TestFixtureSetUp]
        public void TestSetUp()
        {
            routes = new RouteCollection();
            MvcApplication.RegisterRoutes(routes);
        }
        [TestFixtureTearDown]
        public void TestTearDown()
        {
            routes = null;
        }

        [Test]
        public void Default_Route_Blog_Index()
        {
            RouteTestHelper.AssertRoute(routes, "~/",
                new { Controller = "blog", Action = "index" });
        }

        [Test]
        public void Entry_Route()
        {
            RouteTestHelper.AssertRoute(routes, "~/Blog/2012/01/01/some_blog_entry",
                new { Controller = "Blog", Action = "Entry", year = "2012", month = "01", day = "01", title = "some_blog_entry" });
        }

        [Test]
        public void List_Entries_By_Day()
        {
            RouteTestHelper.AssertRoute(routes, "~/Blog/2012/01/01",
                new { Controller = "Blog", Action = "ListByDay", year = "2012", month = "01", day = "01" });
        }

        [Test]
        public void List_Entries_By_Month()
        {
            RouteTestHelper.AssertRoute(routes, "~/Blog/2012/01",
                new { Controller = "Blog", Action = "ListByMonth", year = "2012", month = "01" });
        }

        [Test]
        public void List_Entries_By_Year()
        {
            RouteTestHelper.AssertRoute(routes, "~/Blog/2012",
                new { Controller = "Blog", Action = "ListByYear", year = "2012" });
        }

        [Test]
        public void Category_By_Id()
        {
            RouteTestHelper.AssertRoute(routes, "~/Category/jQuery",
                new { Controller = "Category", Action = "Listing", categoryValue = "jQuery" });
        }

        [Test]
        public void Contact()
        {
            RouteTestHelper.AssertRoute(routes, "~/Contact",
                new { Controller = "Contact", Action = "Index" });
        }

        [Test]
        public void About()
        {
            RouteTestHelper.AssertRoute(routes, "~/About",
                new { Controller = "About", Action = "Index" });
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Ignore_axd_files()
        {
            RouteTestHelper.AssertRoute(routes, "~/some.axd",
                new { Controller = "", Action = "" });
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Ignore_axd_files_with_something_behind()
        {
            RouteTestHelper.AssertRoute(routes, "~/some.axd/with?something.behind",
                new { Controller = "", Action = "" });
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Ignore_xml_files()
        {
            RouteTestHelper.AssertRoute(routes, "~/wlwmanifest.xml",
                new { Controller = "", Action=""});
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
            Assert.IsNotNull(routeData, "Expected to find the route");

            foreach (PropertyValue property in GetProperties(expectations))
            {
                Assert.IsTrue(string.Equals(property.Value.ToString(),
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
