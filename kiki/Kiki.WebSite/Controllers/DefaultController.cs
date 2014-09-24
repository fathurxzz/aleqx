using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Kiki.DataAccess.Repositories;

namespace Kiki.WebSite.Controllers
{
    public class DefaultController : Controller
    {
        public string CurrentLangCode { get; protected set; }

        protected readonly ISiteRepository _repository;

        public DefaultController(ISiteRepository repository)
        {
            _repository = repository;
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            CurrentLangCode = requestContext.RouteData.Values["lang"] as string;
            if (CurrentLangCode != null)
            {
                ViewBag.Lang = CurrentLangCode;
                var ci = new CultureInfo(CurrentLangCode);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }

            base.Initialize(requestContext);
        }
    }
}
