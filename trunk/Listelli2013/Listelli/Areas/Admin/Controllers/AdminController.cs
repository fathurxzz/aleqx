using System.Web.Mvc;
using Listelli.Controllers;

namespace Listelli.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public abstract class AdminController : DefaultController
    {
        //public Language CurrentLang
        //{
        //    get
        //    {
        //        if (WebSession.CurrentLanguage == null)
        //        {
        //            using (var context = new SiteContainer())
        //            {
        //                var lang = context.Language.First(l => l.Code == "ru");
        //                WebSession.CurrentLanguage = lang;
        //                return lang;
        //            }
        //        }
        //        return WebSession.CurrentLanguage;
        //    }
        //}

        //protected override void Initialize(RequestContext requestContext)
        //{
        //    CultureInfo ci = new CultureInfo("ru");

        //    Thread.CurrentThread.CurrentCulture = ci;
        //    base.Initialize(requestContext);
        //}

    }
}
