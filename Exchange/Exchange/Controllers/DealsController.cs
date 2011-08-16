using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;

namespace Exchange.Controllers
{
    public class DealsController : Controller
    {
        private IEnumerable<DealPresentation> GetDeals(IDbConnection conn)
        {
            var dFactory = new DealFactory(conn);
            var dList = dFactory.GetDelasPresentation(WebSession.DateFrom,WebSession.DateTo).Where(d => d.Status == DealStatus.Sent);
            ViewData["CurrencyList"] = Helper.ExtractCurrency(dList.Cast<ICurrencyFilter>());
            dList = dList.Where(d => (d.CurId == WebSession.SelectedCurrency || WebSession.SelectedCurrency == 0)&&(d.OfficeId == WebSession.CurrentUser.OfficeId || d.ParentOfficeId == WebSession.CurrentUser.OfficeId));
            return dList;
        }

        public ActionResult Index(int? curId)
        {
            if (curId.HasValue)
                WebSession.SelectedCurrency = curId.Value;
            using (var conn = DbConnector.Connection)
            {
                return View(GetDeals(conn));
            }
        }

    }
}
