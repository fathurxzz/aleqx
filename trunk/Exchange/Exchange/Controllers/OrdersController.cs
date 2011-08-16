using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Enum;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;
using Exchange.Models.Views;

namespace Exchange.Controllers
{
    public class OrdersController : Controller
    {
        private List<CustomMessage> _messages = new List<CustomMessage>();

        //
        // GET: /Order/


        private IEnumerable<Order> GetOrders(IDbConnection conn)
        {
            var orderFactory = new OrderFactory(conn);

            var oList = orderFactory.GetOrders(WebSession.Date);
            ViewData["CurrencyList"] = Helper.ExtractCurrency(oList.Cast<ICurrencyFilter>());
            ViewData["OperList"] = Helper.ExtractOperation(oList.Cast<IOperationFilter>());
            oList = oList.Where(o =>

                (o.CurId == WebSession.SelectedCurrency || WebSession.SelectedCurrency == 0) &&
                (o.Operation == WebSession.Operation || WebSession.Operation == 0)

                ).OrderBy(o => o.Operation);
            return oList;
        }


        public ActionResult Index(int? curId, int? operId)
        {
            WebSession.SetValues(new FilterValuesUpdater { CurId = curId, OperId = operId }, out _messages);
            using (var conn = DbConnector.Connection)
            {
                return View(GetOrders(conn));
            }
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            using (var conn = DbConnector.Connection)
            {
                FormAction formAction = form.GetFormAction();
                int[] ids = form.CollectObjectIds<int>();
                var oFactory = new OrderFactory(conn);
                switch (formAction)
                {
                    case FormAction.Delete:
                        {
                            foreach (Order order in ids.Select(id => oFactory.GetOrder(id, false)).Where(order => order.DealsCount == 0))
                            {
                                if (!oFactory.Delete(order))
                                {
                                    _messages.Add(new CustomMessage
                                    {
                                        Text = oFactory.ErrorMessage,
                                        Type = MessageType.Error
                                    });
                                }
                            }
                        }
                        break;
                    case FormAction.SendToBranch:
                        {
                            var orders = ids.Select(id => oFactory.GetOrder(id)).Where(order => order.Status == OrderStatus.Created).ToList();
                            foreach (var order in orders.Where(order => order.Course != 0))
                            {
                                order.SetStatus(OrderStatus.SentToBranch);
                            }

                            if (!oFactory.Save(orders))
                            {
                                _messages.Add(new CustomMessage
                                {
                                    Text = oFactory.ErrorMessage,
                                    Type = MessageType.Error
                                });
                            }
                        }
                        break;
                    case FormAction.SendToBranchAndPayment:
                        {
                            bool paymentsHasSaved = true;
                            var paymentFactory = new PaymentFactory(conn);
                            var orders = ids.Select(id => oFactory.GetOrder(id)).Where(order => order.Status == OrderStatus.Created || order.Status == OrderStatus.SentToBranch).ToList();
                            foreach (var order in orders.Where(order => order.Course != 0 && order.Status != OrderStatus.SentToBranchAndPay))
                            {
                                order.SetStatus(OrderStatus.SentToBranchAndPay);
                                IEnumerable<Payment> payments = order.CreatePayments();
                                
                                if(!paymentFactory.Save(payments))
                                {
                                    _messages.Add(new CustomMessage
                                    {
                                        Text = paymentFactory.ErrorMessage,
                                        Type = MessageType.Error
                                    });
                                    paymentsHasSaved = false;
                                    break;
                                }
                            }

                            if (paymentsHasSaved)
                            {
                                if (!oFactory.Save(orders))
                                {
                                    _messages.Add(new CustomMessage
                                                      {
                                                          Text = oFactory.ErrorMessage,
                                                          Type = MessageType.Error
                                                      });
                                }
                            }
                        }
                        break;

                    case FormAction.BackToCreatedStatus:
                        {
                            var orders = ids.Select(id => oFactory.GetOrder(id)).Where(order => order.Status != OrderStatus.Created).ToList();
                            
                            
                            foreach (var order in orders)
                            {
                                if (order.PaymentsPaidCount > 0)
                                {
                                    _messages.Add(new CustomMessage
                                                      {
                                                          Text =
                                                              "Распоряжение №" + order.Number +
                                                              " нельзя вернуть в статус \"сформированный\" т.к. есть оплаченные проводки по данному распоряжению",
                                                          Type = MessageType.Error
                                                      });
                                }
                                else
                                {
                                    order.SetStatus(OrderStatus.Created);
                                    var pFactory = new PaymentFactory(conn);
                                    pFactory.Delete(order.Id);
                                }

                            }

                            
                            


                            if (!oFactory.Save(orders))
                            {
                                _messages.Add(new CustomMessage
                                {
                                    Text = oFactory.ErrorMessage,
                                    Type = MessageType.Error
                                });
                            }


                        }
                        break;
                }
                ViewData["messages"] = _messages;
                return View(GetOrders(conn));
            }
        }



        private static Order GetOrder(IDbConnection conn, int id)
        {
            var orderFactory = new OrderFactory(conn);
            return orderFactory.GetOrder(id);
        }


        public ActionResult Details(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                return View(GetOrder(conn, id));
            }
        }

        [HttpPost]
        public ActionResult Details(Order o, FormCollection form)
        {
            using (var conn = DbConnector.Connection)
            {
                FormAction formAction = form.GetFormAction();

                int[] ids = form.CollectObjectIds<int>();

                switch (formAction)
                {
                    case FormAction.Delete:
                        {
                            var orderFactory = new OrderFactory(conn);
                            Order order = orderFactory.GetOrder(o.Id);
                            order.RemoveDealFromOrder(ids);

                            if (!orderFactory.Save(order))
                            {
                                _messages.Add(new CustomMessage
                                {
                                    Text = orderFactory.ErrorMessage,
                                    Type = MessageType.Error
                                });

                                ViewData["messages"] = _messages;
                                return View(GetOrder(conn, o.Id));
                            }

                            if (order.Deals.Count == 0)
                                return RedirectToAction("Index");
                        }
                        break;
                }
                return View(GetOrder(conn, o.Id));
            }
        }

        public ActionResult UpdateCourse(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                return View(new OrderFactory(conn).GetOrder(id, false));
            }
        }

        [HttpPost]
        public ActionResult UpdateCourse(Order model)
        {
            using (var conn = DbConnector.Connection)
            {
                var oFactory = new OrderFactory(conn);
                Order order = oFactory.GetOrder(model.Id);
                order.UpdateCourse(model.Course);

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
            return RedirectToAction("Index");
        }


        public ActionResult SplitDeal(int id, int orderId)
        {
            using (var conn = DbConnector.Connection)
            {
                var oFactory = new OrderFactory(conn);
                Order order = oFactory.GetOrder(orderId);
                Deal deal = order.Deals.Where(d => d.Id == id).First();

                var matchedOrders = GetMatchedOrders(deal, oFactory);
                var model = new SplitDeal
                                {
                                    DealId = deal.Id,
                                    OldSum = deal.DocSum,
                                    MatchedOrders = matchedOrders,
                                    CurrentOrder = order,
                                    OrderId = order.Id
                                };


                return View(model);
            }
        }

        private static List<Order> GetMatchedOrders(Deal deal, OrderFactory oFactory)
        {
            return oFactory.GetOrders(DateTime.Now).Where(o => o.CurId == deal.CurId && o.Operation == deal.Operation && o.OperSign == deal.OperSign).ToList();
        }

        [HttpPost]
        public ActionResult SplitDeal(SplitDeal model, decimal newSum)
        {
            using (var conn = DbConnector.Connection)
            {

                var oFactory = new OrderFactory(conn);
                Order order = oFactory.GetOrder(model.OrderId);
                Deal currentDeal = order.Deals.Where(d => d.Id == model.DealId).First();
                if (model.NewOrderDealSum > 0)
                {
                    if (model.NewOrderDealSum + newSum > currentDeal.DocSum)
                    {
                        _messages.Add(new CustomMessage { Type = MessageType.Error, Text = "Сумма сделки в новом распоряжении + остаточная сумма текущей сделки не должна превышать изначальную сумму текущей сделки" });
                        ViewData["messages"] = _messages;

                        model.CurrentOrder = order;
                        model.MatchedOrders = GetMatchedOrders(currentDeal, oFactory);
                        return View("SplitDeal", model);
                    }
                }
                else
                {
                    if (newSum < currentDeal.DocSum)
                    {
                        currentDeal.DeltaSum = newSum - currentDeal.DocSum;
                        currentDeal.DocSum = newSum;
                        oFactory.Save(order);
                    }
                }

                return RedirectToAction("Details", new { id = model.OrderId });
            }
        }


        public ActionResult Edit(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var order = new OrderFactory(conn).GetOrder(id, false);
                return View(order);
            }
        }

        [HttpPost]
        public ActionResult Edit(Order model)
        {
            using (var conn = DbConnector.Connection)
            {

                var oFactory = new OrderFactory(conn);
                Order order = oFactory.GetOrder(model.Id, false);

                TryUpdateModel(order, new[] { "Info", "InfoForBranch", "InfoForPay", "DealWithNBU" });

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
            return RedirectToAction("Index");
        }
    }
}
