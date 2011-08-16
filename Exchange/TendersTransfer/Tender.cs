using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Exchange.Models;

namespace TendersTransfer
{
    class Tender
    {
        public static IEnumerable<Exchange.Models.Tender> GetTenders(IDbConnection conn, DateTime dateFrom, DateTime dateTo)
        {
            string query = "exec tnd_txTenders ?,?";
            return conn.ReadAsList(r => Init(new Exchange.Models.Tender(), r), query, dateFrom, dateTo);
        }


        public static Exchange.Models.Tender Init(Exchange.Models.Tender t, IDataRecord dr)
        {

            t.Status = dr.GetValue<TenderStatus>("rec_status");
            t.DocSum = dr.GetValue<decimal>("doc_sum");
            t.InitSum = dr.GetValue<decimal>("init_sum");
            t.Rest = dr.GetValue<decimal>("rest");
            t.Course = dr.GetValue<decimal>("course");
            t.CourseOrient = dr.GetValue<decimal>("course_orient");
            t.CreateDate = dr.GetValue<DateTime>("time_create");
            t.Operation = dr.GetValue<EOperation>("oper_id");
            t.OperSign = dr.GetValue("oper_sign", EOperSign.ClientCash);
            t.CurId = dr.GetValue<int>("cur_id");
            t.CurName = dr.GetValue<string>("cur_name").Trim();
            t.Nls = dr.GetValue<string>("nls");
            t.CancelInfo = dr.GetValue("cancel_info", string.Empty).Trim();
            t.ClientName = dr.GetValue<string>("client").Trim();
            t.ClientCode = dr.GetValue<string>("client_code").Trim();
            t.Commission = dr.GetValue<decimal>("com_branch", 0);
            t.OfficeId = dr.GetValue<int>("office_id");
            t.OfficeName = dr.GetValue<string>("office_name").Trim();
            t.Podrid = dr.GetValue<string>("podrid").Trim();
            t.InfoFromBranch = dr.GetValue("info", string.Empty).Trim();
            t.SellType = dr.GetValue<SellType>("sell_type");
            t.UserId = dr.GetValue<int>("user_id");
            t.SendDate = dr.GetValue<DateTime?>("date_send");
            t.InfoForBranch = dr.GetValue("info_cen", string.Empty).Trim();
            t.UserName = dr.GetValue("user_name", string.Empty).Trim();
            t.TenderDate = dr.GetValue<DateTime>("tndr_date");
            t.IncludeInTotal = dr.GetValue<bool>("is_incl_svod");
            t.Purpose = dr.GetValue("meta", string.Empty).Trim();
            t.IsLabeled = dr.GetValue<bool>("is_mark");
            t.ExecuteSign = dr.GetValue<bool>("sign_proc");
            t.ClientCntrCode = dr.GetValue<string>("client_cntr_code");
            t.ClientSubjId = dr.GetValue<string>("client_sbj_id");
            t.CourseMorSign = dr.GetValue("course_mor_sign", false);
            t.ClientUahPodrid = dr.GetValue<string>("client_uah_podrid");
            t.ClientUahNls = dr.GetValue<string>("client_uah_nls");
            t.ClientCurrPodrid = dr.GetValue<string>("client_curr_podrid");
            t.ClientCurrNls = dr.GetValue<string>("client_curr_nls");
            return t;
        }


        public void Save()
        {

        }
    }
}
