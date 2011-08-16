using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;
using Exchange.Models.MVC;

namespace Exchange.Controllers
{
    [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerRegion })]
    public class BranchTendersController : Controller
    {
        private List<CustomMessage> _messages = new List<CustomMessage>();
        //
        // GET: /BranchTenders/
        
        private static IEnumerable<Tender> GetTenderList(IDbConnection conn)
        {
            var tenderFactory = new TenderFactory(conn);
            return tenderFactory.GetTendersByModerator(WebSession.DateFrom, WebSession.DateTo,WebSession.CurrentUser.Id);
        }


        private IEnumerable<Tender> GetTenders(IDbConnection conn)
        {
            var tList = GetTenderList(conn);
            ViewData["CurrencyList"] = Helper.ExtractCurrency(tList.Cast<ICurrencyFilter>());
            ViewData["OperList"] = Helper.ExtractOperation(tList.Cast<IOperationFilter>());

            tList = tList.Where(t =>
                (t.Operation == WebSession.Operation || WebSession.Operation == 0) &&
                (t.CurId == WebSession.SelectedCurrency || WebSession.SelectedCurrency == 0)
                
                );
            return tList;
        }

        public ActionResult Index(int? curId, int? status, string dateFrom, string dateTo, int? operId)
        {
            if (curId.HasValue)
                WebSession.SelectedCurrency = curId.Value;
            if (status.HasValue)
                WebSession.Status = status.Value;
            if (!string.IsNullOrEmpty(dateFrom))
            {
                DateTime date;
                if (DateTime.TryParse(dateFrom, out date))
                {
                    WebSession.DateFrom = date;
                }
                else
                {
                    _messages.Add(new CustomMessage { Type = MessageType.Error, Text = Helper.GetResourceString("ConvertingDataError") });
                }
            }
            if (!string.IsNullOrEmpty(dateTo))
            {
                DateTime date;
                if (DateTime.TryParse(dateTo, out date))
                {
                    WebSession.DateTo = date;
                }
                else
                {
                    _messages.Add(new CustomMessage { Type = MessageType.Error, Text = Helper.GetResourceString("ConvertingDataError") });
                }
            }
            if (operId.HasValue)
                WebSession.Operation = (EOperation)operId.Value;

            ViewData["messages"] = _messages;
            using (var conn = DbConnector.Connection)
            {
                return View(GetTenders(conn));
            }
        }

        public ActionResult Edit(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                return View(new TenderFactory(conn).GetTender(id));
            }
        }

        [HttpPost]
        public ActionResult Edit(Tender model)
        {
            using (var conn = DbConnector.Connection)
            {
                var tFactory = new TenderFactory(conn);
                var tender = tFactory.GetTender(model.Id);
                TryUpdateModel(tender, new[] { "CourseOrient", "Commission","CourseMorSign"});
                if (!tFactory.Save(tender))
                {
                    _messages.Add(new CustomMessage
                    {
                        Text = tFactory.ErrorMessage,
                        Type = MessageType.Error
                    });

                    ViewData["messages"] = _messages;
                    return View(model);
                }
                return RedirectToAction("Index");
            }
        }


    }
}
