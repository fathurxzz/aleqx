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
    public class TendersController : Controller
    {
        private List<CustomMessage> _messages = new List<CustomMessage>();

        #region BranchLevel

        private static void SetClientFields(FormCollection form, Tender t)
        {
            switch (t.OperSign)
            {
                case EOperSign.ClientCash:
                    {

                        if (string.IsNullOrEmpty(form["clientName"]) || string.IsNullOrEmpty(form["cntrCode"]) ||
                            string.IsNullOrEmpty(form["subjId"]) || string.IsNullOrEmpty(form["okpo"]))
                            return;

                        t.ClientName = form["clientName"];
                        t.ClientSubjId = form["subjId"];
                        t.ClientCode = form["okpo"];
                        t.ClientCntrCode = form["cntrCode"];
                    }
                    break;
                case EOperSign.CurrencyPosition:
                    t.ClientName = WebSession.CurrentUser.OfficeName;
                    t.ClientCode = WebSession.CurrentUser.OfficeOkpo;
                    break;
            }
        }



        private IEnumerable<Tender> GetTenders(IDbConnection conn)
        {
            var tList = GetTenderList(conn).ToList();
            ViewData["CurrencyList"] = Helper.ExtractCurrency(tList.Cast<ICurrencyFilter>());
            ViewData["StatusList"] = Helper.GetStatuses(tList);
            ViewData["tenderTotalsSummary"] = tList.GetTenderTotalSumByCurrency();   //Tender.GetTotalVolumeInfoForBranch(conn, DateTime.Now);
            ViewData["OperList"] = Helper.ExtractOperation(tList.Cast<IOperationFilter>());
            
            tList = tList.Where(t => 
                (t.Operation == WebSession.Operation || WebSession.Operation == 0) && 
                (t.CurId == WebSession.SelectedCurrency || WebSession.SelectedCurrency == 0) && 
                ((int)t.Status == WebSession.Status || WebSession.Status == -100)&&
                (t.OfficeId==WebSession.CurrentUser.OfficeId||t.ParentOfficeId==WebSession.CurrentUser.OfficeId)
                ).ToList();
            return tList;
        }


        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOfficeBranch })]
        public ActionResult Index(int? curId, int? status, string dateFrom, string dateTo, int? operId)
        {
            

            if (curId.HasValue)
                WebSession.SelectedCurrency = curId.Value;
            if (status.HasValue)
                WebSession.Status = status.Value;
            if (!string.IsNullOrEmpty(dateFrom))
            {
                DateTime date;
                if(DateTime.TryParse(dateFrom,out date))
                {
                    WebSession.DateFrom = date;
                }
                else
                {
                    _messages.Add(new CustomMessage{Type = MessageType.Error,Text = Helper.GetResourceString("ConvertingDataError")});
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
                WebSession.Operation = (Models.EOperation)operId.Value;

            ViewData["messages"] = _messages;
            using (var conn = DbConnector.Connection)
            {
                return View(GetTenders(conn));
            }
        }

        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOfficeBranch })]
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            using (var conn = DbConnector.Connection)
            {
                int[] tenderIds = form.CollectObjectIds<int>();
                if (tenderIds.Length > 0)
                {
                    FormAction formAction = form.GetFormAction();

                    var tenderFactory = new TenderFactory(conn);


                    var tList = tenderFactory.GetTenders(tenderIds);

                    CustomMessage message = null;
                    foreach (var tender in tList)
                    {
                        string msg;
                        switch (formAction)
                        {

                            case FormAction.Send:
                                {

                                    if (tender.SendToCentre(out msg))
                                    {
                                        message = new CustomMessage
                                        {
                                            Type = MessageType.Info,
                                            Text = Helper.GetResourceString("Tender") + " №" + tender.Id + " " + Helper.GetResourceString("HasSent")
                                        };
                                    }
                                    else
                                    {
                                        message = new CustomMessage
                                        {
                                            Type = MessageType.Error,
                                            Text = Helper.GetResourceString("Tender") + " №" + tender.Id + " " + Helper.GetResourceString("NotSent") + ": " + msg
                                        };
                                    }
                                }
                                break;
                            case FormAction.Delete:
                                {
                                    if (tender.Delete(out msg))
                                    {
                                        message = new CustomMessage
                                        {
                                            Type = MessageType.Info,
                                            Text = Helper.GetResourceString("Tender") + " №" + tender.Id + " " + Helper.GetResourceString("Deleted")
                                        };
                                    }
                                    else
                                    {
                                        message = new CustomMessage
                                        {
                                            Type = MessageType.Error,
                                            Text = Helper.GetResourceString("Tender") + " №" + tender.Id + " " + Helper.GetResourceString("NotDeleted") + ": " + msg
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
                    ViewData["messages"] = _messages;
                }
                return View(GetTenders(conn));
            }
        }


        private Tender GetTender(int? id, Tender tender, IDbConnection conn)
        {
            if (id.HasValue)
            {
                var tenderFactory = new TenderFactory(conn);
                tender = tenderFactory.GetTender(id.Value);
            }

            
            if (tender!=null && tender.OperSign == EOperSign.ClientCash)
            {
                ViewData["client"] = new Client
                {
                    ClientCode = tender.ClientCode,
                    ClientName = tender.ClientName,
                    ContrCode = Convert.ToInt32(tender.ClientCntrCode),
                    SubjId = Convert.ToInt32(tender.ClientSubjId)
                };
            }

            IEnumerable<UserCurrency> userCurrencies = WebSession.CurrentUser.CurrencyList.Where(c => c.AllowedProcessing);
            var userCurrencyList = userCurrencies.Select(currency => new SelectListItem { Text = currency.CurId.ToString(), Value = currency.CurId.ToString(), Selected = tender != null && tender.CurId == currency.CurId }).ToList();
            ViewData["userCurrencies"] = userCurrencyList;

            return tender;
        }



        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOfficeBranch })]
        public ActionResult Create(int? id)
        {
            using (var conn = DbConnector.Connection)
            {
                return View(GetTender(id, null,conn));
            }
        }

        [HttpPost]
        [CustomAuthorize(Roles = new[] { SiteRoles.MasterAdmin, SiteRoles.DealerFrontOfficeBranch })]
        public ActionResult Create(Tender t, FormCollection form)
        {
            t.OfficeId = WebSession.CurrentUser.OfficeId;
            t.ParentOfficeId = WebSession.CurrentUser.ParentOfficeId;
            t.InitSum = t.Rest = t.DocSum = Convert.ToDecimal(form["DocSum"].Replace(".", ","));

            /*decimal course;
            decimal.TryParse(form["Course"], out course);
            */

            if (string.IsNullOrEmpty(form["Course"]))
                t.CourseOrient = t.Course = 0;
            else
                t.CourseOrient = t.Course = Convert.ToDecimal(form["Course"].Replace(".", ","));

            t.Commission = Convert.ToDecimal(form["Commission"].Replace(".", ","));
            t.Auction = false;
            t.UserId = WebSession.CurrentUser.Id;
            t.Status = TenderStatus.Created;
            t.CreateDate = DateTime.Now;

            t.Operation = (Models.EOperation)Convert.ToInt32(form["drOper"]);
            t.OperSign = (EOperSign)Convert.ToInt32(form["drOperSign"]);
            t.CurId = Convert.ToInt32(form["drCurrency"]);
            t.Nls = form["drNls"];


            if (t.Operation == Models.EOperation.Sell)
            {
                t.SellType = (SellType)Convert.ToInt32(form["drSellType"]);
            }

            SetClientFields(form, t);

            using (var conn = DbConnector.Connection)
            {
                var tenderFactory = new TenderFactory(conn);

                if (!tenderFactory.Save(t))
                {
                    _messages.Add(new CustomMessage
                                      {
                                          Text = tenderFactory.ErrorMessage,
                                          Type = MessageType.Error
                                      });
                    ViewData["messages"] = _messages;
                    return View(GetTender(null, t, conn));
                }
            }
            
            return RedirectToAction("Index");
        }

        #endregion

        private static IEnumerable<Tender> GetTenderList(IDbConnection conn)
        {
            var tenderFactory = new TenderFactory(conn);
            return tenderFactory.GetTenders(WebSession.DateFrom,WebSession.DateTo);
        }

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult GetClients(string podrid, string okpo, string contrCode)
        {
            if (string.IsNullOrEmpty(podrid))
            {
                ViewData["errorMsg"] = "podrid cannot be empty";
                return View("ClientList");
            }
            if (okpo == "0")
                okpo = "";
            if (contrCode == "0")
                contrCode = "";
            IEnumerable<Client> clients = Client.GetClientsFromOdb(podrid, okpo, contrCode);
            return View("ClientList", clients);
        }
    }
}
