using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace shopping
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UrunDuzenle",
                url: "UrunDuzenle/{urunadi}-{id}",
                defaults: new { controller = "Admin", action = "UrunDuzenle", id = UrlParameter.Optional, urunadi = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Urun",
                url: "Urun/{kategoriadi}/{urunadi}-{id}",
                defaults: new { controller = "Home", action = "Urun", id = UrlParameter.Optional, urunadi = UrlParameter.Optional, kategoriadi = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
