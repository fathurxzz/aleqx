using System;
using System.Collections.Generic;
using System.Configuration;
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
    [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOffice })]
    public class TransactionsController : Controller
    {
        private List<CustomMessage> _messages = new List<CustomMessage>();
        //
        // GET: /Transactions/

        public ActionResult Index(int? curId, int? status, string dateFrom, string dateTo, string podrid, bool? matching)
        {

            WebSession.SetValues(new FilterValuesUpdater
                                          {
                                              TransactionStatus = status,
                                              DateFrom = dateFrom,
                                              DateTo = dateTo,
                                              Podrid = podrid,
                                              CurId = curId
                                          }, out _messages);

            ViewData["StatusList"] = Helper.GetTransactionStatuses();
            using (var conn = DbConnector.Connection)
            {
                var trFactory = new TransactionFactory(conn);

                //if (matching.HasValue && matching.Value)
                //Transaction.LoadTransactions(conn);

                var trList = trFactory.GetTransactions(WebSession.DateFrom, WebSession.DateTo);

                if (matching.HasValue && matching.Value)
                {
                    _messages.Add(MatchingTransactions(conn, trList));
                }

                ViewData["CurrencyList"] = Helper.ExtractCurrency(trList.Cast<ICurrencyFilter>());

                trList = trList.Where(t =>
                    (t.CurId == WebSession.SelectedCurrency || WebSession.SelectedCurrency == 0)


                    && (t.Podrid == WebSession.Podrid || WebSession.Podrid == "")
                    && ((!t.Deleted && WebSession.TransactionStatus != (int)FilterStatus.Cancelled) || (WebSession.TransactionStatus == (int)FilterStatus.Cancelled && t.Deleted))
                    && (!t.KvDate.HasValue || WebSession.TransactionStatus != (int)FilterStatus.NotMatched)



                    );

                ViewData["messages"] = _messages;

                return View(trList);
            }
        }


        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult Matching()
        {
            int transactionMatchingInterval = WebSession.ExchangeSettings.TransactionMatchingInterval;

            /*if (!int.TryParse(ConfigurationManager.AppSettings["TransactionMatchingInterval"],out transactionMatchingInterval))
            {

                return View("CustomMessage",
                            new CustomMessage
                                {
                                    Type = MessageType.Error,
                                    Text = "Ошибка при попытке квитовки заявок. Не указан интервал квитовки (параметр TransactionMatchingInterval в веб-конфиге)"
                                }
                    );
            }
            */

            if(transactionMatchingInterval==0)
            {
                return View("CustomMessage",
                            new CustomMessage
                            {
                                Type = MessageType.Error,
                                Text = "Ошибка при попытке квитовки заявок. Интервал квитовки должен быть больше 0"
                            }
                    );
            }

            TimeSpan ts = DateTime.Now - WebSession.LastMatchingTime;
            if (ts.Minutes < transactionMatchingInterval && ts.TotalMilliseconds != 0)
            {
                return null;
            }
            WebSession.LastMatchingTime = DateTime.Now;

            //Transaction.LoadTransactions(conn);

            using (var conn = DbConnector.Connection)
            {
                var trFactory = new TransactionFactory(conn);
                var trList = trFactory.GetTransactions(DateTime.Now);
                var message = MatchingTransactions(conn, trList);
                return View("CustomMessage", message);
            }
        }

        private static CustomMessage MatchingTransactions(IDbConnection conn, IEnumerable<Transaction> trList)
        {
            int matchedTransactionCount;
            return !Transaction.Matching(conn, trList.Where(t => !t.TenderId.HasValue).ToList(), out matchedTransactionCount)
                ? new CustomMessage { Type = MessageType.Error, Text = Transaction.ErrorMessage }
                : new CustomMessage { Type = MessageType.Info, Text = "Сквитовано заявок: " + matchedTransactionCount };
        }

        public ActionResult Edit(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var trFactory = new TransactionFactory(conn);
                return View(trFactory.GetTransaction(id));
            }
        }

        [HttpPost]
        public ActionResult Edit(Transaction model)
        {
            using (var conn = DbConnector.Connection)
            {
                var trFactory = new TransactionFactory(conn);
                var tr = trFactory.GetTransaction(model.Id);
                TryUpdateModel(tr, new[] { "Deleted", "KvSum" });
                trFactory.Update(tr);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var trFactory = new TransactionFactory(conn);
                var t = trFactory.GetTransaction(id);
                t.Deleted = true;
                trFactory.Update(t);
                return RedirectToAction("Index");
            }
        }

    }
}
