using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Entities;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;
using Exchange.Models.MVC;

namespace Exchange.Controllers
{
    [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice })]
    public class CentreDealsController : Controller
    {
        private List<CustomMessage> _messages = new List<CustomMessage>();

        private IEnumerable<Deal> GetDeals(IDbConnection conn)
        {
            var dFactory = new DealFactory(conn);
            var dList = dFactory.GetDeals(WebSession.Date).Where(d => d.Status == DealStatus.Sent).ToList();
            ViewData["CurrencyList"] = Helper.ExtractCurrency(dList.Cast<ICurrencyFilter>());
            ViewData["OperList"] = Helper.ExtractOperation(dList.Cast<IOperationFilter>());
            ViewData["OperSignList"] = Helper.ExtractOperationSign(dList.Cast<IOperSignFilter>());


            dList = dList.Where(
                d => (d.CurId == WebSession.SelectedCurrency || WebSession.SelectedCurrency == 0)
                    && (d.Operation == WebSession.Operation || WebSession.Operation == 0)
                && (d.OperSign == WebSession.OperSign || WebSession.OperSign == 0)
                ).ToList();
            return dList;
        }


        public ActionResult Index(int? operId, int? curId, string date, int? operSign)
        {
            WebSession.SetValues(
                new FilterValuesUpdater { Date = date, CurId = curId, OperId = operId, OperSign = operSign },
                out _messages);
            using (var conn = DbConnector.Connection)
            {
                return View(GetDeals(conn));
            }
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, decimal? courseMor)
        {
            using (var conn = DbConnector.Connection)
            {
                int[] dealIds = form.CollectObjectIds<int>();

                if (courseMor.HasValue)
                {
                    if (WebSession.SelectedCurrency == 0 || WebSession.Operation == 0 || WebSession.OperSign == 0)
                    {
                        _messages.Add(new CustomMessage
                                          {
                                              Type = MessageType.Error,
                                              Text = "Для пакетного изменения курса MOR необходимо выбрать валюту, операцию и признак операции"
                                          });
                    }
                    else
                    {
                        var dFactory = new DealFactory(conn);
                        IEnumerable<Deal> deals = dFactory.GetDeals(dealIds).ToList();

                        foreach (Deal deal in deals)
                        {
                            deal.CourseMor = courseMor.Value;
                        }

                        if (!dFactory.Update(deals))
                        {
                            _messages.Add(new CustomMessage
                            {
                                Type = MessageType.Error,
                                Text = dFactory.ErrorMessage
                            });
                        }
                    }
                }

                ViewData["messages"] = _messages;
                return View(GetDeals(conn));
            }

        }


        public ActionResult Edit()
        {
            return View();
        }

    }
}
