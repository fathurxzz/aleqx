using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models
{
    public class Payment:ICurrencyFilter,IOperationFilter
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Paid { get; set; }
        public DateTime? PayDate { get; set; }
        public int CurId { get; set; }
        public string CurDef{get;set;}
        public string CurName{get;set;}
        public decimal DocSum { get; set; }
        public EOperation Operation { get; set; }
        public string OperName{get;set;}
        public int DealCurId { get; set; }
        public string OsFileName { get; set; }
        public int UserId { get; set; }
        public decimal Course { get; set; }
        public ExchangeMarket ExCode { get; set; }
        public int OfficeId { get; set; }
        public decimal Equivalent { get; set; }
        public EOperSign OperSign { get; set; }
        public string Podrid { get; set; }
        public string DocNls { get; set; }
        public string DocNlsK { get; set; }
        public string Edrpou { get; set; }
        public string OfficeName { get; set; }



        public static Payment InitPayment(Payment p, IDataRecord r)
        {
            p.Id = r.GetValue<int>("id");
            p.OrderId = r.GetValue<int>("order_id");
            p.CreateDate = r.GetValue<DateTime>("create_date");
            p.Paid = r.GetValue<bool>("paid");
            p.PayDate = r.GetValue<DateTime?>("pay_date");
            p.CurId = r.GetValue<int>("cur_id");
            p.DocSum = r.GetValue<decimal>("doc_sum");
            p.Operation = r.GetValue<EOperation>("oper_id");
            p.DealCurId = r.GetValue<int>("deal_cur_id");
            p.OsFileName = r.GetValue<string>("os_file_name");
            p.UserId = r.GetValue<int>("user_id");
            p.Course = r.GetValue<decimal>("course");
            p.ExCode = r.GetValue<ExchangeMarket>("ex_code");
            p.OfficeId = r.GetValue<int>("office_id");
            p.Equivalent = r.GetValue<decimal>("equiv");
            p.OperSign = r.GetValue<EOperSign>("oper_sign");
            p.Podrid = r.GetValue<string>("podrid");
            p.Edrpou = r.GetValue<string>("edrpou");
            p.OfficeName = r.GetValue<string>("office_name");
            p.CurDef = r.GetValue<string>("cur_def");
            p.CurName = r.GetValue<string>("cur_name");
            p.OperName = Helper.GetResourceString(p.Operation.ToString());
            p.DocNls = r.GetValue<string>("nls");
            p.DocNlsK = r.GetValue<string>("nlsk");



            if (r.ContainsColumn("nls"))
                p.DocNls = r.GetValue<string>("nls");
            if (r.ContainsColumn("nlsk"))
                p.DocNlsK = r.GetValue<string>("nlsk");



            return p;
        }
    }
}