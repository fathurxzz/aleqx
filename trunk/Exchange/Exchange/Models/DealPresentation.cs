using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class DealPresentation : Deal
    {
        /// <summary>
        /// Номер розпорядження
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Номер розпорядження
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Дата відправки у філію
        /// </summary>
        public DateTime OrderSendDate { get; set; }

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

        public DealPresentation(IDataRecord r):base(r)
        {
            if (r.ContainsColumn("order_send_date"))
                OrderSendDate = r.GetValue<DateTime>("order_send_date");
            if (r.ContainsColumn("order_date"))
                OrderDate = r.GetValue<DateTime>("order_date");
            if (r.ContainsColumn("order_number"))
                OrderNumber = r.GetValue<string>("order_number");

            if (r.ContainsColumn("info"))
                Info = r.GetValue<string>("info");
            if (r.ContainsColumn("info_for_pay"))
                InfoForPay = r.GetValue<string>("info_for_pay");
            if (r.ContainsColumn("info_for_branch"))
                InfoForBranch = r.GetValue<string>("info_for_branch");
        }
    }
}