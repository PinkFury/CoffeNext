using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoffeeNext
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Registration",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Registration", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Login" }
            );
            routes.MapRoute(
                name: "About",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "About" }
            );
            routes.MapRoute(
                name: "Menu",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Menu" }
            );
            routes.MapRoute(
                name: "Adress",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Adress" }
            );
            routes.MapRoute(
                name: "Account",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Account", id = UrlParameter.Optional }
            );
        }
    }
}
