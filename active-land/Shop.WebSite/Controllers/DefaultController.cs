using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.EntityFramework;

namespace Shop.WebSite.Controllers
{
    public class DefaultController : BaseController
    {
        public string CurrentLangCode { get; protected set; }

        //public Language CurrentLang { get; protected set; }
        protected int CurrentLangId { get; set; }

        private readonly Dictionary<string, int> _langs = new Dictionary<string, int> {{"ru", 1}, {"en", 2}};


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                CurrentLangCode = requestContext.RouteData.Values["lang"] as string;
                CurrentLangId = _langs[CurrentLangCode];
                var ci = new CultureInfo(CurrentLangCode);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
            base.Initialize(requestContext);
        }

    }
}
