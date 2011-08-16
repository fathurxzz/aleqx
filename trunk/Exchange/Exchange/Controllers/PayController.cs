using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Exchange.Models;
using Exchange.Models.Enum;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;
using ExchangeExtensions.OsFile;
using OsFilePayment = ExchangeExtensions.OsFile.Payment;


namespace Exchange.Controllers
{
    public class PayController : Controller
    {
        //
        // GET: /Pay/

        private List<CustomMessage> _messages = new List<CustomMessage>();

        [OutputCache(NoStore = true, VaryByParam = "*", Duration = 1)]
        public ActionResult Index(int? curId, int? operId)
        {
            WebSession.SetValues(
                new FilterValuesUpdater {/* Status = status, Date = date, Podrid = podrid,*/ CurId = curId, OperId = operId/*, Course = course, OperSign = operSign */},
                out _messages);

            using (var conn = DbConnector.Connection)
            {
                var oFactory = new OrderFactory(conn);
                var orders = oFactory.GetOrders(WebSession.Date).Where(o => o.Status == OrderStatus.SentToBranchAndPay).ToList();
                ViewData["CurrencyList"] = Helper.ExtractCurrency(orders.Cast<ICurrencyFilter>());
                ViewData["OperList"] = Helper.ExtractOperation(orders.Cast<IOperationFilter>());
                orders = orders.Where(o => (o.CurId == WebSession.SelectedCurrency || WebSession.SelectedCurrency == 0) && (o.Operation == WebSession.Operation || WebSession.Operation == 0)).ToList();
                return View(orders);
            }
        }


        private IEnumerable<Models.Payment> Getpayments(IDbConnection conn)
        {
            var pFactory = new PaymentFactory(conn);
            IEnumerable<Models.Payment> payments = pFactory.GetPayments(WebSession.Date).ToList();
            ViewData["CurrencyList"] = Helper.ExtractCurrency(payments.Cast<ICurrencyFilter>());
            ViewData["OperList"] = Helper.ExtractOperation(payments.Cast<IOperationFilter>());
            return payments.Where(p => (p.CurId == WebSession.SelectedCurrency || WebSession.SelectedCurrency == 0) && (p.Operation == WebSession.Operation || WebSession.Operation == 0)).ToList();
        }


        public ActionResult Payments(int? curId, int? operId)
        {
            WebSession.SetValues(
                new FilterValuesUpdater {/* Status = status, Date = date, Podrid = podrid,*/ CurId = curId, OperId = operId/*, Course = course, OperSign = operSign */},
                out _messages);
            using (var conn = DbConnector.Connection)
            {
                return View(Getpayments(conn));
            }
        }


        [HttpPost]
        public ActionResult Payments(FormCollection form)
        {
            using (var conn = DbConnector.Connection)
            {
                int[] paymentIds = form.CollectObjectIds<int>();
                FormAction formAction = form.GetFormAction();
                if (formAction == FormAction.Delete)
                {
                    var pFactory = new PaymentFactory(conn);
                    pFactory.UnPaid(paymentIds);
                }

                return View(Getpayments(conn));
            }
        }




        public ActionResult Details(int id)
        {
            using (var conn = DbConnector.Connection)
            {
                var pFactory = new PaymentFactory(conn);
                IEnumerable<Models.Payment> payments = pFactory.GetPayments(id);
                ViewData["orderId"] = id;

                return View(payments);
            }
        }






        public ActionResult CreateOs(FormCollection form, int orderId)
        {
            var pList = new PaymentList();

            int[] paymentIds = form.CollectObjectIds<int>();

            if (paymentIds.Count() == 0)
                return RedirectToAction("Details", new { id = orderId });

            using (var conn = DbConnector.Connection)
            {
                var order = new OrderFactory(conn).GetOrder(orderId);

                string osFileName = GetOsFileName(order);
                ViewData["filename"] = osFileName;


                var pFactory = new PaymentFactory(conn);
                var payments = pFactory.GetPayments(paymentIds).ToList();
                int cnt = 0;
                foreach (var item in payments)
                {
                    decimal paymentSum = item.Operation == EOperation.Buy ? item.DocSum : item.Equivalent;
                    cnt++;

                    var p = new OsFilePayment
                    {
                        TaskId = "2.13.4.",
                        DocNm = item.Podrid == "300012" ? "1" : "2",
                        DocNp = "813",
                        DocDk = "1",
                        DocVob = "6",
                        DocNd = cnt.ToString(),
                        DocMFO = item.Podrid,
                        DocNls = item.DocNls,
                        KodCountryNls = "804",
                        DocNlsk = item.DocNlsK,
                        KodCountryNlsk = "804",
                        DocOkpoPol = item.Edrpou,
                        DocNaimpol = item.OfficeName,
                        DocS = paymentSum.ToString(),
                        CurId = item.CurId.ToString(),
                        DocDataa = DateTime.Now.ToOsFileFormatDate(),
                        DocData = DateTime.Now.ToOsFileFormatDate(),
                        DocReceiveinbank = DateTime.Now.ToOsFileFormatDate(),
                        DocNplat = NaznachPlatPay(item.DocSum, item.Operation, item.DealCurId, item.CreateDate, item.ExCode, item.Course.ToString(), conn),
                        DocRow = cnt.ToString(),
                        DocHighRow = "0",
                        FlagZo = "0",
                        CodBanka = "1",
                        PltId = "0",
                        Creator = !string.IsNullOrEmpty(WebSession.CurrentUser.UserIdOdb) ? WebSession.CurrentUser.UserIdOdb : ""
                    };

                    AddDopParams(p, item);
                    pList.Add(p);
                    item.Paid = true;
                    item.PayDate = DateTime.Now;
                    item.OsFileName = osFileName;
                }

                if (!pFactory.Update(payments))
                {
                    throw new Exception(pFactory.ErrorMessage);
                }

            }




            ViewData["content"] = pList.GetContent();
            return View();
        }

        private string GetOsFileName(Order order)
        {
            const string prefix = "OS";

            // TODO: переписать
            string n = order.Number;
            if (n.Length == 2)
                n = "0" + n;
            ///////////////////////

            if (order.Operation == EOperation.Sell)
            {
                return prefix + n + order.CurId + ".t" + DateTime.Now.ToString("dd");
            }
            return prefix + order.CurId + DateTime.Now.ToString("dd").Substring(1, 1) + DateTime.Now.ToString("HH").Substring(1, 1) + DateTime.Now.ToString("mm").Substring(0, 1) + "." + DateTime.Now.ToString("mm").Substring(1, 1) + DateTime.Now.ToString("ss");


        }


        private string NaznachPlatPay(decimal paymentSum, EOperation oper, int curId, DateTime date, ExchangeMarket exchangeMarket, string course, IDbConnection conn)
        {
            if (oper == EOperation.Buy)
            {
                return "Зараховується " + paymentSum + " " + Helper.GetCurName(curId, conn) + " куплених " + date.ToShortDateString() + " на " + Helper.GetExchangeMarketNameF(exchangeMarket) + " за курсом " + course + ".";
            }
            return "Зараховується гривневий еквівалент від продажу " + paymentSum + " " + Helper.GetCurName(curId, conn) + " " + date.ToShortDateString() + " на " + Helper.GetExchangeMarketNameF(exchangeMarket) + " за курсом " + course;
        }


        private void AddDopParams(OsFilePayment p, Models.Payment payment)
        {
            switch (payment.Operation)
            {
                case EOperation.Buy:
                    switch (payment.OperSign)
                    {
                        case EOperSign.CurrencyPosition:
                            // ознака платника/отримувача – приймає значення 1 (назва з анкети);450(699)D - 1059(1197)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "1059", KeyFieldId = "1197", DocDk = "1" });

                            //дата врегулювання – зазначається поточна дата; 257(152) - 3051(2039)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "3051", KeyFieldId = "2039", DocDk = DateTime.Now.Date.ToShortDateString() });

                            //підстава для придбання – заявка на купівлю валюти; 257(162) - 3051(2049)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "3051", KeyFieldId = "2049", DocDk = "завка на купівлю валюти" });

                            // інституційний сектор економіки – приймає значення Z – інше;257(672)D - 3051(3164)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "3051", KeyFieldId = "3164", DocDk = "Z" });

                            //ознака платника/отримувача - приймає значення 1 (назва з анкети);451(699)K- 1059(1197)
                            p.DopParams.Add(new DopParameter { Gr = "K", KeyTitleId = "1059", KeyFieldId = "1197", DocDk = "1" });

                            //інституційний сектор економіки – приймає значення Z – інше. 51(672) K- 3065(3164)
                            p.DopParams.Add(new DopParameter { Gr = "K", KeyTitleId = "3065", KeyFieldId = "3164", DocDk = "Z" });

                            // дата виникнення заборгованності - зазначається поточна дата ; 257(139)  D   - 3051(2027)
                            //p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "3051", KeyFieldId = "2027", DocDk = DateTime.Now.Date.ToShortDateString() });

                            break;

                        case EOperSign.ClientCash:
                            //ознака платника/отримувача – приймає значення 1 (назва з анкети); 450(699)D - 1059(1197)
                            //p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "1059", KeyFieldId = "1197", DocDk = "1" });

                            //дата договору – зазначається поточна дата; 182(139)D - 2021(1040)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "2021", KeyFieldId = "1040", DocDk = DateTime.Now.Date.ToShortDateString() });

                            //найменування дебітора – Промінвестбанк; 182(137)D - 2021(2025)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "2021", KeyFieldId = "2025", DocDk = "Промінвестбанк" });

                            //інституційний сектор економіки – приймає значення Z – інше; 182(672)D - 2021(3164)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "2021", KeyFieldId = "3164", DocDk = "Z" });

                            //ознака платника/отримувача - приймає значення 1 (назва з анкети); 451(699) K- 1059 (1197)
                            //p.DopParams.Add(new DopParameter { Gr = "K", KeyTitleId = "1059", KeyFieldId = "1197", DocDk = "1" });

                            // дата врегулювання - зазначається поточна дата. 181(696)DK - 2034(2098)
                            //p.DopParams.Add(new DopParameter { Gr = "DK", KeyTitleId = "2034", KeyFieldId = "2098", DocDk = DateTime.Now.Date.ToShortDateString() });


                            // дата виникнення заборгованності - зазначається поточна дата ; 182(139)  D   - 2021(2027)
                            //p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "2021", KeyFieldId = "2027", DocDk = DateTime.Now.Date.ToShortDateString() });

                            //причина виникнення заборгованності - продаж валюти; 182(140)  D   - 2021(2028)
                            //p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "2021", KeyFieldId = "2028", DocDk = "купівля валюти" });

                            //дата врегулювання - зазначається поточна дата. 182(152)  D - 2021(2039)
                            //p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "2021", KeyFieldId = "2039", DocDk = DateTime.Now.Date.ToShortDateString() });
                            p.DopParams.Add(new DopParameter { Gr = "DK", KeyTitleId = "2034", KeyFieldId = "2098", DocDk = DateTime.Now.Date.ToShortDateString() });


                            // Код підрозділу, у якому обслуговується (актив/пасив)  954(1139) D 1059(1230)
                            //p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "1059", KeyFieldId = "1230", DocDk = "314" });

                            // Код підрозділу, у якому обслуговується (актив/пасив)  955(1139) K 1059(1230)
                            //p.DopParams.Add(new DopParameter { Gr = "K", KeyTitleId = "1059", KeyFieldId = "1230", DocDk = "314" });


                            break;
                    }
                    break;

                case EOperation.Sell:
                    switch (payment.OperSign)
                    {
                        case EOperSign.CurrencyPosition:
                            //ознака платника/отримувача – приймає значення 1 (назва з анкети); 450(699)D - 1059(1197)
                            //p.DopParams.Add(new DopParameter { Gr = "K", KeyTitleId = "1059", KeyFieldId = "1197", DocDk = "1" });

                            //дата врегулювання – зазначається поточна дата;257(152)D - 3051(2039)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "3051", KeyFieldId = "2039", DocDk = DateTime.Now.Date.ToShortDateString() });

                            //підстава для придбання – заявка на купівлю валюти;257(162)D - 3051(2049)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "3051", KeyFieldId = "2049", DocDk = "завка на купівлю валюти" });

                            //інституційний сектор економіки – приймає значення Z – інше;51(672) K- 3065(3164)
                            p.DopParams.Add(new DopParameter { Gr = "K", KeyTitleId = "3065", KeyFieldId = "3164", DocDk = "Z" });

                            //ознака платника/отримувача - приймає значення 1 (назва з анкети);450(699)D - 1059(1197)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "1059", KeyFieldId = "1197", DocDk = "1" });

                            //інституційний сектор економіки – приймає значення Z – інше. 257(672)D - 3051(3164)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "3051", KeyFieldId = "3164", DocDk = "Z" });

                            //вид заборгованності – приймає значення 1;257(79)D - 3051(1038)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "3051", KeyFieldId = "1038", DocDk = "1" });

                            // дата виникнення заборгованності - зазначається поточна дата ; 257(139)D - 3051(2027)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "3051", KeyFieldId = "2027", DocDk = DateTime.Now.Date.ToShortDateString() });


                            break;

                        case EOperSign.ClientCash:
                            //ознака платника/отримувача - приймає значення 1 (назва з анкети);450(699)D - 1059(1197)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "1059", KeyFieldId = "1197", DocDk = "1" });

                            // вид заборгованності – приймає значення 1; 182(79)    D   - 2021(1038)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "2021", KeyFieldId = "1038", DocDk = "1" });

                            //найменування дебітора – Промінвестбанк;182(137)D - 2021(2025)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "2021", KeyFieldId = "2025", DocDk = "Промінвестбанк" });

                            //інституційний сектор економіки – приймає значення Z – інше; 257(672)  D - 2021(3164)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "2021", KeyFieldId = "3164", DocDk = "Z" });

                            // дата виникнення заборгованності - зазначається поточна дата ; 182(139)  D   - 2021(2027)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "2021", KeyFieldId = "2027", DocDk = DateTime.Now.Date.ToShortDateString() });

                            //причина виникнення заборгованності - продаж валюти; 182(140)  D   - 2021(2028)
                            p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "2021", KeyFieldId = "2028", DocDk = "продаж валюти" });

                            //дата врегулювання - зазначається поточна дата. 181(696)  DK - 2034(2098)
                            p.DopParams.Add(new DopParameter { Gr = "DK", KeyTitleId = "2034", KeyFieldId = "2098", DocDk = DateTime.Now.Date.ToShortDateString() });


                            // Код підрозділу, у якому обслуговується (актив/пасив)  954(1139) D 1059(1230)
                            //p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "1059", KeyFieldId = "1230", DocDk = "314" });

                            // Код підрозділу, у якому обслуговується (актив/пасив)  955(1139) K 1059(1230)
                            //p.DopParams.Add(new DopParameter { Gr = "K", KeyTitleId = "1059", KeyFieldId = "1230", DocDk = "314" });




                            //ознака платника/отримувача – приймає значення 1 (назва з анкети);450(699)D - 1059(1197)
                            //p.DopParams.Add(new DopParameter { Gr = "K", KeyTitleId = "1059", KeyFieldId = "1197", DocDk = "1" });

                            //дата договору – зазначається поточна дата;257(139)- 3051(2027)
                            //p.DopParams.Add(new DopParameter { Gr = "D", KeyTitleId = "3051", KeyFieldId = "1040", DocDk = DateTime.Now.Date.ToShortDateString() });

                            //дата врегулювання - зазначається поточна дата.182(152)D - 2021(2039) 181(696)DK - 2021(2039)
                            //p.DopParams.Add(new DopParameter { Gr = "DK", KeyTitleId = "2034", KeyFieldId = "2098", DocDk = DateTime.Now.Date.ToShortDateString() });

                            //

                            break;
                    }
                    break;
            }
        }
    }
}
