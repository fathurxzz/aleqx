using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Listelli.Controllers;
using Listelli.Models;

namespace Listelli.Areas.Admin.Controllers
{
    public abstract class AdminController : BaseController
    {
        public Language CurrentLang
        {
            get
            {
                if (WebSession.CurrentLanguage == null)
                {
                    using (var context = new SiteContainer())
                    {
                        var lang = context.Language.First(l => l.Code == "ru");
                        WebSession.CurrentLanguage = lang;
                        return lang;
                    }
                }
                return WebSession.CurrentLanguage;
            }
        }

        //protected override void Initialize(RequestContext requestContext)
        //{
        //    CultureInfo ci = new CultureInfo("ru");

        //    Thread.CurrentThread.CurrentCulture = ci;
        //    base.Initialize(requestContext);
        //}

    }
}
