using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NewVision.UI.Models;

namespace NewVision.UI.Controllers
{
    public class DefaultController : Controller
    {
        protected string CurrentLangCode { get; set; }
        protected SiteLanguage CurrentLang { get; set; }


        public DefaultController()
        {
            ViewBag.Title = "New Vision Pro";
        }

        protected override void Initialize(RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                CurrentLangCode = requestContext.RouteData.Values["lang"] as string;

                switch (CurrentLangCode)
                {
                    case "ru":
                        CurrentLang = SiteLanguage.ru;
                        break;
                    case "ua":
                        CurrentLang = SiteLanguage.ua;
                        break;
                    case "en":
                        CurrentLang = SiteLanguage.en;
                        break;
                    default:
                        CurrentLang = SiteLanguage.ru;
                        break;
                }

                ViewBag.Locale = CurrentLangCode;

                var ci = new CultureInfo(CurrentLangCode == "ua" ? "uk" : CurrentLangCode);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }

            base.Initialize(requestContext);
        }
    }
}
