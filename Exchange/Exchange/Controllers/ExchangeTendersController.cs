using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Entities;
using Exchange.Models.Enum;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;
using Exchange.Models.MVC;
using Exchange.Models.Views;

namespace Exchange.Controllers
{
    public class ExchangeTendersController : Controller
    {
        private List<CustomMessage> _messages = new List<CustomMessage>();
        //
        // GET: /ExchangeTenders/

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice })]
        public ActionResult Index(int? operId, int? curId, int? status, string date, string course, string podrid, int? operSign)
        {
            WebSession.SetValues(
                new FilterValuesUpdater
                    {Status = status, Date = date, Podrid = podrid, CurId = curId, OperId = operId, Course = course,OperSign = operSign},
                out _messages);

            using (var conn = DbConnector.Connection)
            {
                return View(GetTenders(conn));
            }
        }

        private IEnumerable<Tender> GetTenders(IDbConnection conn)
        {
            var tenderFactory = new TenderFactory(conn);
            IEnumerable<Tender> tList = tenderFactory.GetTenders(WebSession.Date).ToList();

            tList = tList.Where(t => t.Operation == WebSession.Operation).ToList();

            ViewData["CurrencyList"] = Helper.ExtractCurrency(tList.Cast<ICurrencyFilter>());
            ViewData["OperSignList"] = Helper.ExtractOperationSign(tList.Cast<IOperSignFilter>());
            ViewData["StatusList"] = Helper.GetCentreStatuses();
            ViewData["totalVolumeInfo"] = Tender.GetTotalVolumeInfoForCentre(conn, DateTime.Now, WebSession.Operation, WebSession.Status);
            tList = tList.Where(t => (t.CurId == WebSession.SelectedCurrency || WebSession.SelectedCurrency == 0)&&(t.Podrid==WebSession.Podrid||WebSession.Podrid=="")&&(t.OperSign==WebSession.OperSign||(int)WebSession.OperSign==0)).ToList();
            ViewData["CourseList"] = Helper.ExctractCourse(tList.Cast<ICourseFilter>());
            tList = tList.Where(t =>(t.CourseMorSign&&WebSession.Course=="MOR")|| (t.CourseOrient.ToString().Replace(",",".")== WebSession.Course||WebSession.Course=="all"));
            ViewData["messages"] = _messages;
            return tList.ApplyCentreStatusFilter((FilterStatus)WebSession.Status);
            
        }

        [HttpPost]
        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice })]
        public ActionResult Index(FormCollection form)
        {
            int[] tenderIds = form.CollectObjectIds<int>();
            FormAction formAction = form.GetFormAction();
            CustomMessage message = null;

            using (var conn = DbConnector.Connection)
            {
                var tenderFactory = new TenderFactory(conn);
                var tList = tenderFactory.GetTenders(tenderIds);


                string msg;
                if (formAction == FormAction.Execute)
                {
                    ExecuteMethod executeMethod;

                    switch (tList.Count)
                    {
                        case 0:
                            executeMethod = ExecuteMethod.ByCourse;
                            break;
                        case 1:
                            executeMethod = ExecuteMethod.Single;
                            break;
                        default:
                            executeMethod = ExecuteMethod.Multiply;
                            break;
                    }

                    if (executeMethod==ExecuteMethod.ByCourse)
                    {
                        tList = tenderFactory.GetTenders(WebSession.Date).
                            Where(
                                t =>
                                t.CurId == WebSession.SelectedCurrency && 
                                t.Operation == WebSession.Operation &&
                                t.OperSign == WebSession.OperSign && 
                                t.Rest > 0 && 
                                t.Status == TenderStatus.Sent)
                                .ToList();
                    }

                    if (tList.ValidateTendersBeforeExecute(executeMethod, out msg))
                    {
                        var tex = new TenderExecute
                        {
                            Tenders = tList,
                            TenderIds = tList.TenderIdsToString(),
                            OrderDate = WebSession.Date.ToShortDateString(),
                            ExecuteMethod = executeMethod,
                            ExchangeMarket = new List<SelectListItem>
                                                 {
                                                     new SelectListItem
                                                     {
                                                         Text = Helper.GetResourceString(ExchangeMarket.VVR.ToString()),
                                                         Value = ((int)ExchangeMarket.VVR).ToString()
                                                     },
                                                    new SelectListItem
                                                    {
                                                        Text = Helper.GetResourceString(ExchangeMarket.MVRU.ToString()),
                                                        Value = ((int)ExchangeMarket.MVRU).ToString()
                                                    }
                                                 }
                        };


                        tex.CurId = WebSession.SelectedCurrency;
                        tex.OperSign = (int)WebSession.OperSign;
                        tex.OperId = (int) WebSession.Operation;
                        tex.Operation = WebSession.Operation;

                        if (tList.Count > 0)
                        {
                            if (executeMethod == ExecuteMethod.Single || executeMethod == ExecuteMethod.Multiply)
                            {
                                if (executeMethod == ExecuteMethod.Single)
                                {
                                    tex.DocSum = tList[0].Rest;
                                    tex.Course = tList[0].Course;
                                }
                                tex.ExistedOrders = new OrderFactory(conn).GetOrders(DateTime.Now).Where(o => o.CurId == tex.CurId && o.Operation == tex.Operation && o.OperSign == (EOperSign)tex.OperSign).ToList();
                            }
                        }
                        else
                        {
                            tex.OperId = (int) WebSession.Operation;
                        }

                        if (executeMethod == ExecuteMethod.ByCourse)
                        {
                            tex.Courses = tList.Select(t => t.CourseOrient).Distinct().OrderBy(t => t).ToList();
                        }


                        return View("Execute", tex);
                    }

                    _messages.Add(new CustomMessage
                                      {
                                          Type = MessageType.Error,
                                          Text = msg
                                      });
                }
                else
                {

                    foreach (var tender in tList)
                    {
                        switch (formAction)
                        {
                            case FormAction.Cancel:
                                {

                                    if (tender.Cancel(out msg))
                                    {
                                        message = new CustomMessage
                                                      {
                                                          Type = MessageType.Info,
                                                          Text = Helper.GetResourceString("Tender") + " №" + tender.Id +
                                                              " " + Helper.GetResourceString("Cancelled")
                                                      };
                                    }
                                    else
                                    {
                                        message = new CustomMessage
                                                      {
                                                          Type = MessageType.Error,
                                                          Text = Helper.GetResourceString("Tender") + " №" + tender.Id +
                                                              " " + Helper.GetResourceString("NotCancelled") + ": " +
                                                              msg
                                                      };
                                    }
                                }
                                break;
                        }

                        if (tender.WasEdited)
                        {
                            tenderFactory.Save(tender);
                            _messages.Add(!string.IsNullOrEmpty(tenderFactory.ErrorMessage)
                                              ? new CustomMessage
                                                    {
                                                        Text = tenderFactory.ErrorMessage,
                                                        Type = MessageType.Error
                                                    }
                                              : message);
                        }
                        else
                        {
                            if (message != null)
                                _messages.Add(message);
                        }
                    }


                }
                ViewData["messages"] = _messages;
                return View(GetTenders(conn));
            }
        }


        [HttpPost]
        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice })]
        public ActionResult Execute(TenderExecute model, FormCollection form)
        {
            using (var conn = DbConnector.Connection)
            {
                var tFactory = new TenderFactory(conn);
                var oFactory = new OrderFactory(conn);
                List<Tender> tList;
                switch (model.ExecuteMethod)
                {
                    case ExecuteMethod.Multiply:
                    case ExecuteMethod.Single:
                        {
                            int[] tenderIds = model.TenderIds.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToArray();
                            tList = tFactory.GetTenders(tenderIds);
                            var order = oFactory.GetOrder(Convert.ToDateTime(model.OrderDate), model.OrderNumber, model.CurId, model.OperSign, model.OperId) ??
                                        new Order
                                            {
                                                Number = model.OrderNumber,
                                                ExchangeMarket = Order.GetExchangeMarketFromOrderNumber(model.OrderNumber),
                                                CurId = model.CurId,
                                                Operation = (EOperation)model.OperId,
                                                OperSign = (EOperSign)model.OperSign
                                            };



                            foreach (var tender in tList)
                            {
                                Deal deal = tender.CreateDeal(model.DocSum);
                                if (!order.AddDealToOrder(deal))
                                {
                                    _messages.Add(new CustomMessage
                                                      {
                                                          Text = "Заявка №" + deal.TenderId + " в распоряжение не отправлена: " + order.ErrorMessage,
                                                          Type = MessageType.Error
                                                      });
                                }
                            }



                            if (!oFactory.Save(order))
                            {
                                _messages.Add(new CustomMessage
                                                  {
                                                      Text = oFactory.ErrorMessage,
                                                      Type = MessageType.Error
                                                  });

                                ViewData["messages"] = _messages;

                                return View(model);
                            }
                        }
                        break;
                    case ExecuteMethod.ByCourse:
                        {
                            // TODO: дописать реализацию выполнения заявок ByCourse

                            int curId = model.CurId;
                            var exchangeMarket = (ExchangeMarket) Convert.ToInt32(form["ExchangeMarket"]);
                            var oper = (EOperation)model.OperId;
                            var operSign = (EOperSign) model.OperSign;

                            tList = tFactory.GetTenders(WebSession.Date).
                                Where(t => t.CurId == curId && t.Operation == oper && t.OperSign==operSign&&t.Rest>0&&t.Status==TenderStatus.Sent).ToList();


                            //List<decimal> courses = tList.Select(t => t.CourseOrient).Distinct().OrderBy(t=>t).ToList();


                            decimal[] courses = form.CollectObjectIds<decimal>();


                            /*List<KeyValuePair<EOperSign,decimal>> operSignCourse = 
                                tList.GroupBy(x => new
                                                       {
                                                           x.CourseOrient, x.OperSign
                                                       }, 
                                (t, g) => new KeyValuePair<EOperSign, decimal>(t.OperSign,t.CourseOrient))
                                .ToList();
                            */


                            var orderNumber = oFactory.GetMaxOrderNumber(exchangeMarket, Convert.ToDateTime(model.OrderDate));
                            

                            foreach (decimal course in courses)
                            {
                                orderNumber++;

                                var order = new Order
                                                {
                                                    Number = Order.GetOrderNumber(exchangeMarket, orderNumber),
                                                    ExchangeMarket = exchangeMarket,
                                                    CurId = curId,
                                                    Operation = oper,
                                                    OperSign = operSign,
                                                    Course = course
                                                };

                                foreach (var tender in tList.Where(t => t.CourseOrient == course).ToList())
                                {
                                    Deal deal = tender.CreateDeal();
                                    if (!order.AddDealToOrder(deal))
                                    {
                                        _messages.Add(new CustomMessage
                                                          {
                                                              Text =
                                                                  "Заявка №" + deal.TenderId +
                                                                  " в распоряжение не отправлена: " + order.ErrorMessage,
                                                              Type = MessageType.Error
                                                          });
                                    }
                                }

                                if (!oFactory.Save(order))
                                {
                                    _messages.Add(new CustomMessage
                                    {
                                        Text = oFactory.ErrorMessage,
                                        Type = MessageType.Error
                                    });

                                    ViewData["messages"] = _messages;

                                    return View(model);
                                }
                            }
                        }
                        break;
                }
            }
            return RedirectToAction("Index");
        }



        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice })]
        public ActionResult Edit(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                return View(new TenderFactory(conn).GetTender(id));
            }
        }

        [HttpPost]
        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice })]
        public ActionResult Edit(Tender model)
        {
            using (var conn = DbConnector.Connection)
            {
                var tFactory = new TenderFactory(conn);
                var tender = tFactory.GetTender(model.Id);
                TryUpdateModel(tender, new[] { "CourseOrient", "CourseMorSign", "Commission", "ExtraCommissionSum", "InfoForBranch", "CancelInfo","IncludeInTotal","IsLabeled" });
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