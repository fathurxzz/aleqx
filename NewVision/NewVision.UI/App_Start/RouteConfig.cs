﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewVision.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            routes.MapRoute(
               "Login",
               "login",
               new { controller = "Auth", action = "Login" },
               new[] { "NewVision.UI.Controllers" }
            );

            routes.MapRoute(
               "Logout",
               "logout",
               new { controller = "Auth", action = "Logout" },
               new[] { "NewVision.UI.Controllers" }
            );


            routes.MapRoute(
               "contacts",
               "{lang}/contacts",
               new { controller = "Home", action = "Contacts" },
               new { lang = @"ru|ua|en" },
               new[] { "NewVision.UI.Controllers" }
           );

            routes.MapRoute(
               "feedback",
               "{lang}/feedback",
               new { controller = "Home", action = "Feedback" },
               new { lang = @"ru|ua|en" },
               new[] { "NewVision.UI.Controllers" }
           );

            routes.MapRoute(
               "events",
               "{lang}/events",
               new { controller = "Home", action = "Events" },
               new { lang = @"ru|ua|en" },
               new[] { "NewVision.UI.Controllers" }
           );

            routes.MapRoute(
               "partnership",
               "{lang}/partnership",
               new { controller = "Home", action = "Partnership" },
               new { lang = @"ru|ua|en" },
               new[] { "NewVision.UI.Controllers" }
           );
            routes.MapRoute(
              "news",
              "{lang}/news",
              new { controller = "Home", action = "News" },
              new { lang = @"ru|ua|en" },
              new[] { "NewVision.UI.Controllers" }
          );
            routes.MapRoute(
              "newsDetails",
              "{lang}/news-details/{id}",
              new { controller = "Home", action = "NewsDetails", id = UrlParameter.Optional },
              new { lang = @"ru|ua|en" },
              new[] { "NewVision.UI.Controllers" }
          );
            routes.MapRoute(
              "media",
              "{lang}/media",
              new { controller = "Home", action = "Media" },
              new { lang = @"ru|ua|en" },
              new[] { "NewVision.UI.Controllers" }
          );

            routes.MapRoute(
             "artistSearch",
             "{lang}/artists/tags/{tagsId}",
             new { controller = "Home", action = "Authors" },
             new { lang = @"ru|ua|en" },
             new[] { "NewVision.UI.Controllers" }
         );

            routes.MapRoute(
              "authors",
              "{lang}/artists",
              new { controller = "Home", action = "Authors" },
              new { lang = @"ru|ua|en" },
              new[] { "NewVision.UI.Controllers" }
          );

            routes.MapRoute(
              "authorsAbout",
              "{lang}/about/{id}",
              new { controller = "Home", action = "ArtistAbout" },
              new { lang = @"ru|ua|en" },
              new[] { "NewVision.UI.Controllers" }
          );
            routes.MapRoute(
              "authorsEvents",
              "{lang}/artist-events/{id}",
              new { controller = "Home", action = "ArtistEvents" },
              new { lang = @"ru|ua|en" },
              new[] { "NewVision.UI.Controllers" }
          );


            routes.MapRoute(
             "artistProductsDetails",
             "{lang}/arts/{id}/details",
             new { controller = "Home", action = "ArtistProductsDetails" },
             new { lang = @"ru|ua|en" },
             new[] { "NewVision.UI.Controllers" }
         );

            routes.MapRoute(
              "artistProductsSearch",
              "{lang}/arts/tags/{tagsId}",
              new { controller = "Home", action = "Products" },
              new { lang = @"ru|ua|en" },
              new[] { "NewVision.UI.Controllers" }
          );

            routes.MapRoute(
              "artistProducts",
              "{lang}/arts/{id}",
              new { controller = "Home", action = "ArtistProducts" },
              new { lang = @"ru|ua|en" },
              new[] { "NewVision.UI.Controllers" }
          );

            routes.MapRoute(
              "products",
              "{lang}/arts",
              new { controller = "Home", action = "Products" },
              new { lang = @"ru|ua|en" },
              new[] { "NewVision.UI.Controllers" }
          );

          routes.MapRoute(
              "eventDetails",
              "{lang}/event-details/{id}",
              new { controller = "Home", action = "EventDetails", id = UrlParameter.Optional },
              new { lang = @"ru|ua|en" },
              new[] { "NewVision.UI.Controllers" }
          );

          routes.MapRoute(
          "content",
          "{lang}/{id}",
          new { controller = "Home", action = "SiteContent", lang = "ru" },
          new { lang = @"ru|ua|en" },
          new[] { "NewVision.UI.Controllers" }
          );

          routes.MapRoute(
               "Default1",
               "{lang}",
               new { controller = "Home", action = "Index", lang = "ru" },
               new { lang = @"ru|ua|en" },
              new[] { "NewVision.UI.Controllers" }
          );

            routes.MapRoute(
                 "Default",
                 "{controller}/{action}/{id}",
                 new { controller = "Home", action = "Index", id = UrlParameter.Optional, lang = "ru" },
                 new { lang = @"ru|ua|en" },
                new[] { "NewVision.UI.Controllers" }
            );
        }
    }
}