using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Exchange.Models.Enum;
using Exchange.Models.Helpers;

namespace Exchange.Models
{
    public class Order : ICurrencyFilter, IOperationFilter, IOperSignFilter
    {
        /// <summary>
        /// Код валюти
        /// </summary>
        public int CurId { get; set; }

        /// <summary>
        /// Код валюти (букв.)
        /// </summary>
        public string CurDef { get; set; }

        /// <summary>
        /// Назва валюти
        /// </summary>
        public string CurName { get; set; }

        /// <summary>
        /// Ідинтифікатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Операція
        /// </summary>
        public EOperation Operation { get; set; }

        /// <summary>
        /// Назва операції
        /// </summary>
        public string OperName { get; set; }

        /// <summary>
        /// Ознака операції
        /// </summary>
        public EOperSign OperSign { get; set; }

        /// <summary>
        /// Ознака операції
        /// </summary>
        public string OperSignName { get; set; }

        /// <summary>
        /// Сума валюти
        /// </summary>
        public decimal DocSum { get; set; }

        /// <summary>
        /// Номер
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Валютний ринок
        /// </summary>
        public ExchangeMarket ExchangeMarket { get; set; }

        /// <summary>
        /// Додаткова інформація
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Додаткова інформація для філії
        /// </summary>
        public string InfoForBranch { get; set; }

        /// <summary>
        /// Додаткова інформація для оплати
        /// </summary>
        public string InfoForPay { get; set; }

        /// <summary>
        /// Ознака угоди з НБУ
        /// </summary>
        public bool DealWithNBU { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFakeOrder { get; set; }

        /// <summary>
        /// Гривневий еквівалент
        /// </summary>
        public decimal Equivalent { get; set; }

        /// <summary>
        /// Статус розпорядження
        /// </summary>
        public OrderStatus Status { get; set; }


        public string StatusName { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public decimal Course { get; set; }

        /// <summary>
        /// Дата створення
        /// </summary>
        public DateTime CreateDate { get; set; }


        /// <summary>
        /// Дата відправки у філію
        /// </summary>
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// Кількість угод у розпорядженні
        /// </summary>
        public int DealsCount { get; set; }

        /// <summary>
        /// Кількість платежів у розпорядженні
        /// </summary>
        public int PaymentsCount { get; set; }

        /// <summary>
        /// Кількість оплачених платежів у розпорядженні
        /// </summary>
        public int PaymentsPaidCount { get; set; }

        /// <summary>
        /// Угоди
        /// </summary>
        public List<Deal> Deals = new List<Deal>();


        private string _errMessage;

        public string ErrorMessage
        {
            get
            { return _errMessage; }
        }

        private Deal GetDeal(int tenderId)
        {
            return Deals.FirstOrDefault(deal => deal.TenderId == tenderId);
        }

        private static void MergeDeals(Deal ownDeal, Deal newDeal)
        {
            ownDeal.DocSum += newDeal.DocSum;
            ownDeal.Equivalent = Helper.Round(ownDeal.DocSum * ownDeal.Course, 2);
            ownDeal.DeltaSum += newDeal.DocSum;
        }

        private bool ValidateDeal(Deal d)
        {
            if (d == null)
            {
                return false;
            }

            if (d.CurId != CurId)
            {
                _errMessage = "Валюта заявки не совпадает с валютой распоряжения";
                return false;
            }

            if (d.Operation != Operation)
            {
                _errMessage = "Операция заявки не совпадает с операцией распоряжения";
                return false;
            }

            if (d.OperSign != OperSign)
            {
                _errMessage = "Признак операции заявки не совпадает с признаком операции распоряжения";
                return false;
            }

            return true;
        }

        public bool AddDealToOrder(Deal newDeal)
        {
            if (!ValidateDeal(newDeal))
            {
                return false;
            }

            Deal ownDeal = GetDeal(newDeal.TenderId);
            if (ownDeal != null)
            {
                MergeDeals(ownDeal, newDeal);
            }
            else
            {
                newDeal.OrderId = Id;
                newDeal.ExCode = ExchangeMarket;
                newDeal.Equivalent = Helper.Round(newDeal.DocSum * newDeal.Course, 2);
                if (this.Status == OrderStatus.Created)
                    Deals.Add(newDeal);
            }
            return true;
        }

        public void RemoveDealFromOrder(int[] dealIds)
        {
            foreach (var deal in Deals.Where(deal => deal.Id.In(dealIds)))
            {
                deal.Deleted = true;
            }
        }

        public static ExchangeMarket GetExchangeMarketFromOrderNumber(string orderNumber)
        {
            return (ExchangeMarket)Convert.ToInt16(orderNumber.Substring(0, 1));
        }

        public static string GetOrderNumber(ExchangeMarket exchangeMarket, int number)
        {
            return ((int) exchangeMarket) + number.ToString().PadLeft(2, '0');
        }

        public static Order InitOrder(IDataRecord r)
        {
            var order = new Order
                       {
                           Id = r.GetValue<int>("id"),
                           Number = r.GetValue<string>("number"),
                           Status = r.GetValue<OrderStatus>("status"),
                           CurId = r.GetValue<int>("cur_id"),
                           CurDef = r.GetValue<string>("cur_def"),
                           CurName = r.GetValue<string>("cur_name"),
                           Operation = r.GetValue<EOperation>("oper_id"),
                           DocSum = r.GetValue<decimal>("doc_sum"),
                           Course = r.GetValue<decimal>("course"),
                           Equivalent = r.GetValue<decimal>("equiv"),
                           ExchangeMarket = r.GetValue<ExchangeMarket>("ex_code"),
                           Info = r.GetValue<string>("info"),
                           InfoForPay = r.GetValue<string>("info_for_pay"),
                           InfoForBranch = r.GetValue<string>("info_for_branch"),
                           CreateDate = r.GetValue<DateTime>("create_date"),
                           DealWithNBU = r.GetValue("deal_with_nbu", false),
                           IsFakeOrder = r.GetValue("order_fake", false),
                           DealsCount = r.GetValue<int>("deals_cnt"),
                           SendDate = r.GetValue<DateTime?>("send_date"),
                           OperSign = r.GetValue<EOperSign>("oper_sign"),
                           PaymentsCount = r.GetValue<int>("payments_cnt"),
                           PaymentsPaidCount = r.GetValue<int>("payments_paid_cnt")


                       };

            order.OperName = Helper.GetResourceString(order.Operation.ToString());
            order.StatusName = Helper.GetResourceString(order.Status.ToString());
            return order;
        }


        public void UpdateCourse(decimal newCourse)
        {
            Course = newCourse;
            foreach (var deal in Deals)
            {
                deal.Course = newCourse;
                deal.Equivalent = Helper.Round(deal.DocSum * newCourse, 2);
            }
        }

        public void SetStatus(OrderStatus status)
        {
            if (this.Status == status)
                return;
            Status = status;

            switch (status)
            {
                case OrderStatus.Created:
                    if (SendDate.HasValue)
                        SendDate = null;
                    foreach (var deal in this.Deals)
                    {
                        deal.Status = DealStatus.Created;
                    }
                    break;
                case OrderStatus.SentToBranch:
                    if (!SendDate.HasValue)
                        SendDate = DateTime.Now;
                    foreach (var deal in this.Deals)
                    {
                        deal.Status = DealStatus.Sent;
                        deal.CourseClient = deal.Course;
                    }
                    break;
                case OrderStatus.SentToBranchAndPay:
                    if (!SendDate.HasValue)
                        SendDate = DateTime.Now;
                    foreach (var deal in this.Deals)
                    {
                        deal.Status = DealStatus.Sent;
                        deal.CourseClient = deal.Course;
                    }
                    break;
            }
        }

        public IEnumerable<Payment> CreatePayments()
        {
            return this.Deals.GroupBy(
                x => new { x.CurId, x.Operation, x.ParentOfficeId, x.ExCode, x.Course,x.OperSign },
                (d, g) => new Payment
                                {
                                    CurId = d.Operation == EOperation.Buy ? d.CurId : 980,
                                    DealCurId = d.CurId,
                                    Operation = d.Operation,
                                    DocSum = g.Sum(x => x.DocSum),
                                    Equivalent = g.Sum(x => x.Equivalent),
                                    OfficeId = d.ParentOfficeId,
                                    OrderId = this.Id,
                                    CreateDate = DateTime.Now,
                                    Paid = false,
                                    ExCode = d.ExCode,
                                    Course = d.Course,
                                    UserId = WebSession.CurrentUser.Id,
                                    OperSign = d.OperSign
                                }).ToList();
        }

        public bool ValidateOrder(out int curId, out EOperation oper, out EOperSign operSign, out decimal course, out string errMessage)
        {
            curId = 0;
            oper = EOperation.Buy;
            operSign = EOperSign.ClientCash;
            course = 0;
            errMessage = string.Empty;

            if (Deals.Count == 0)
            {
                errMessage = "В распоряжении нет сделок";
                return false;
            }

            if (!ExchangeMarket.In(new[] { ExchangeMarket.VVR, ExchangeMarket.MVRU }))
            {
                errMessage = "Невозможно определить валютный рынок, проверьте номер распоряжения";
                return false;
            }

            curId = Deals[0].CurId;
            oper = Deals[0].Operation;
            operSign = Deals[0].OperSign;
            course = Deals[0].Course;

            int id = curId;
            if (Deals.Any(deal => id != deal.CurId))
            {
                errMessage = "Распоряжение содержит сделки с разными валютами";
                return false;
            }

            EOperation operation = oper;
            if (Deals.Any(deal => operation != deal.Operation))
            {
                errMessage = "Распоряжение содержит сделки с разными операциями";
                return false;
            }

            EOperSign sign = operSign;
            if (Deals.Any(deal => sign != deal.OperSign))
            {
                errMessage = "Распоряжение содержит сделки с разными типами операций";
                return false;
            }

            decimal rate = course;
            if (Deals.Any(deal => rate != deal.Course))
            {
                course = 0;
            }

            return true;
        }
    }
}