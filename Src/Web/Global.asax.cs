using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using Rosier.Blog.Data;

namespace Rosier.Blog.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{resource}.xml/{*pathInfo}");

            // entry mapping    -- ~/blog/2012/01/01/my_entry_title
            routes.MapRoute(
                "Entry",
                "blog/{year}/{month}/{day}/{title}",
                new { controller = "Blog", action = "Entry" },
                new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}" });

            // entry mapping    -- ~/blog/2012/01/01
            routes.MapRoute(
                "ListByDay",
                "blog/{year}/{month}/{day}",
                new { controller = "Blog", action = "ListByDay" },
                new { year = @"\d{4}", month = @"\d{2}", day = @"\d{2}" });

            // entry mapping    -- ~/blog/2012/01
            routes.MapRoute(
                "ListByMonth",
                "blog/{year}/{month}",
                new { controller = "Blog", action = "ListByMonth" },
                new { year = @"\d{4}", month = @"\d{2}" });

            // entry mapping    -- ~/blog/2012
            routes.MapRoute(
                "ListByYear",
                "blog/{year}",
                new { controller = "Blog", action = "ListByYear" },
                new { year = @"\d{4}" });

            // category mapping -- ~/category/jQuery
            routes.MapRoute(
                "ListByCategory",
                "category/{categoryValue}",
                new { controller="category", action="Listing" });

            // default mapping -- ~/{controller}/{action}/{id}
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Blog", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "NotFound",
                "{*any}",
                new { controller="Blog", action="NotFound"}
                );

        }

        protected void Application_Start()
        {
            // Database.SetInitializer<EntityDataContext>(new DatabaseInitialization());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(typeof(BlogControllerFactory));
        }
    }
}