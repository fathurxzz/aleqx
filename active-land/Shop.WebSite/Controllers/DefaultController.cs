using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Shop.DataAccess.Entities;
using Shop.DataAccess.EntityFramework;
using Shop.DataAccess.Repositories;
using Shop.WebSite.Helpers;

namespace Shop.WebSite.Controllers
{
    public class DefaultController : BaseController
    {
        public string CurrentLangCode { get; protected set; }

        //public Language CurrentLang { get; protected set; }
        protected int CurrentLangId { get; set; }
        protected int DefaultLangId { get; set; }
        protected int DefaultAdminLangId { get; set; }

        //private readonly Dictionary<string, int> _langs = new Dictionary<string, int> {{"ru", 1}, {"en", 2}};


        protected readonly IShopRepository _repository;

        public DefaultController(IShopRepository repository)
        {
            _repository = repository;
        }


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (WebSession.Languages == null)
            {
                WebSession.Languages = _repository.GetLanguages().ToList();
            }


            if (requestContext.RouteData.Values["lang"] != null && requestContext.RouteData.Values["lang"] as string != "null")
            {
                CurrentLangCode = requestContext.RouteData.Values["lang"] as string;
                CurrentLangId = WebSession.Languages.First(l => l.Code == CurrentLangCode).Id;
                DefaultLangId = WebSession.Languages.First(l => l.IsDefault).Id;
                DefaultAdminLangId = WebSession.Languages.First(l => l.IsAdminDefault).Id;

                var ci = new CultureInfo(CurrentLangCode == "ua" ? "uk" : CurrentLangCode);
                Thread.CurrentThread.CurrentUICulture = ci;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            }
            base.Initialize(requestContext);
        }

    }
}
