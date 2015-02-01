using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TeacherReviews
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
             name: "reviews_delete",
             url: "reviews/delete/{id}/{pid}",
             defaults: new { controller = "Review", action = "Delete" }
         );
            routes.MapRoute(
            name: "reviews_clear",
            url: "reviews/clear/{id}",
            defaults: new { controller = "Review", action = "Clear" }
        );

            routes.MapRoute(
              name: "reviews_add",
              url: "reviews/add",
              defaults: new { controller = "Review", action = "Add" }
          );

            routes.MapRoute(
               name: "reviews",
               url: "reviews/{id}",
               defaults: new { controller = "Review", action = "Index" }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
