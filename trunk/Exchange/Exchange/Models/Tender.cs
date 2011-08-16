using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Entities;
using Exchange.Models.Helpers;

namespace Exchange.Models
{
    /// <summary>
    /// Заявка на купівлю/продаж валюти
    /// </summary>
    public class Tender : TenderDeal, ICurrencyFilter, IOperationFilter, IOperSignFilter, ICourseFilter
    {
        /// <summary>
        /// Статус
        /// </summary>
        public TenderStatus Status { get; set; }

        /// <summary>
        /// Початкова сума заявки
        /// </summary>
        public decimal InitSum { get; set; }

        /// <summary>
        /// Залишок
        /// </summary>
        public decimal Rest { get; set; }

        /// <summary>
        /// Курс розрахунку
        /// </summary>
        public decimal CourseOrient { get; set; }

        /// <summary>
        /// Дата відправлення завки до ЦО
        /// </summary>
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// Дата торгу
        /// </summary>
        public DateTime? TenderDate { get; set; }

        /// <summary>
        /// Вид продажу
        /// </summary>
        public SellType? SellType { get; set; }

        /// <summary>
        /// Назва виду продажу
        /// </summary>
        public string SellTypeName { get; private set; }

        /// <summary>
        /// Інформація для філії
        /// </summary>
        public string InfoForBranch { get; set; }

        /// <summary>
        /// Інформація для ЦО
        /// </summary>
        public string InfoFromBranch { get; set; }

        /// <summary>
        /// Причина скасування заявки
        /// </summary>
        public string CancelInfo { get; set; }

        /// <summary>
        /// Ознака включення заявки на обробку
        /// </summary>
        public bool ExecuteSign { get; set; }

        /// <summary>
        /// Ознака включення завки в загальну суму
        /// </summary>
        public bool IncludeInTotal { get; set; }

        /// <summary>
        /// Ознака "відмітки"
        /// </summary>
        public bool IsLabeled { get; set; }

        /// <summary>
        /// Мета купівлі/продажу
        /// </summary>
        public string Purpose { get; set; }



        /// <summary>
        /// Ознака реалізацій за курсом MOR
        /// </summary>
        public bool CourseMorSign { get; set; }

        /// <summary>
        /// Час квитовки заявки
        /// </summary>
        public DateTime? KvDate { get; set; }

        /// <summary>
        /// Рекомендований курс розрахунку з клієнтом
        /// </summary>
        public decimal? CourseClientRecommend { get; set; }



        public bool WasEdited { get; private set; }

        public Tender()
        {

        }

        public override string ToString()
        {
            return "Id: "+Id;
        }

        public Tender(IDataRecord r)
        {
            Id = r.GetValue<int>("id");
            UserId = r.GetValue<int>("user_id");
            UserName = r.GetValue<string>("user_name");
            UserPhone = r.GetValue<string>("user_phone");
            Status = r.GetValue<TenderStatus>("status");
            InitSum = r.GetValue<decimal>("init_sum");
            DocSum = r.GetValue<decimal>("doc_sum");
            Rest = r.GetValue<decimal>("rest");
            Course = r.GetValue<decimal>("course");
            CourseOrient = r.GetValue<decimal>("course_orient");
            CourseMorSign = r.GetValue<bool>("course_mor_sign");
            CreateDate = r.GetValue<DateTime>("create_date");
            SendDate = r.GetValue<DateTime?>("send_date");
            TenderDate = r.GetValue<DateTime?>("tndr_date");
            SellType = r.GetValue<SellType?>("sell_type");
            if (SellType.HasValue)
                SellTypeName = Helper.GetResourceString(SellType.ToString());
            InfoFromBranch = r.GetValue<string>("info_from_branch");
            InfoForBranch = r.GetValue<string>("info_for_branch");
            CancelInfo = r.GetValue<string>("cancel_info");
            ExecuteSign = r.GetValue<bool>("execute_sign");
            IncludeInTotal = r.GetValue<bool>("include_in_total");
            IsLabeled = r.GetValue<bool>("is_labeled");
            Purpose = r.GetValue<string>("purpose");
            Auction = r.GetValue<bool>("auction");
            KvDate = r.GetValue<DateTime?>("kv_date");
            CourseClientRecommend = r.GetValue<decimal?>("course_client_recommend");

            Podrid = r.GetValue<string>("podrid");
            OfficeId = r.GetValue<int>("office_id");
            OfficeName = r.GetValue<string>("office_name");
            ParentOfficeId = r.GetValue<int>("parent_office_id");
            ParentOfficeName = r.GetValue<string>("parent_office_name");
            Operation = r.GetValue<EOperation>("oper_id");
            CurId = r.GetValue<int>("cur_id");
            CurDef = r.GetValue<string>("cur_def");
            CurName = r.GetValue<string>("cur_name");
            Nls = r.GetValue<string>("nls");
            OperSign = r.GetValue<EOperSign>("oper_sign");
            Commission = r.GetValue<decimal>("commission");
            ExtraCommissionSum = r.GetValue<decimal?>("extra_commission_sum");
            ClientCode = r.GetValue<string>("client_code");
            ClientName = r.GetValue<string>("client_name");
            ClientSubjId = r.GetValue<string>("client_subj_id");
            ClientCntrCode = r.GetValue<string>("client_cntr_code");
            ClientUahPodrid = r.GetValue<string>("client_podrid_uah");
            ClientUahNls = r.GetValue<string>("client_nls_uah");
            ClientCurrPodrid = r.GetValue<string>("client_podrid_curr");
            ClientCurrNls = r.GetValue<string>("client_nls_curr");


            OperName = Helper.GetResourceString(Operation.ToString());
            OperSignName = Helper.GetResourceString(OperSign.ToString());
            StatusName = Helper.GetResourceString(Status.ToString());
        }

        public bool SendToCentre(out string message)
        {
            message = "";
            if (Status == TenderStatus.Created)
            {
                Status = TenderStatus.Sent;
                SendDate = DateTime.Now;
                TenderDate = DateTime.Now;
                if (Operation == EOperation.Buy)
                    ExecuteSign = true;
                IncludeInTotal = true;
                WasEdited = true;
                return true;
            }
            if (Status == TenderStatus.Sent)
            {
                message = Helper.GetResourceString("TenderHasAlreadySent");
            }
            if (Status == TenderStatus.Cancelled)
            {
                message = Helper.GetResourceString("TenderCancelled");
            }
            return false;
        }

        public bool Cancel(out string message)
        {
            message = "";
            switch (Status)
            {
                case TenderStatus.Cancelled:
                    {
                        message = Helper.GetResourceString("TenderHasAlreadyCancelled");
                        return false;
                    }
                default:
                    {
                        Status = TenderStatus.Cancelled;
                        WasEdited = true;
                        return true;
                    }
            }
        }

        public bool Delete(out string message)
        {
            message = "";
            if (Status == TenderStatus.Created)
            {
                Status = TenderStatus.ReadyForDelete;
                WasEdited = true;
                return true;
            }
            message = Helper.GetResourceString("TenderCanBeDeleted") + " \"" + Helper.GetResourceString(TenderStatus.Created.ToString()) + "\"";
            return false;
        }

        public static IEnumerable<TotalVolumeInfoEntity> GetTotalVolumeInfoForCentre(IDbConnection connection, DateTime date, EOperation oper, int status)
        {
            SumField field = status == 2 ? SumField.Rest : SumField.DocSum;
            string query = "exec tx_getTotalVolumeInfo ?,?,?";
            return connection.ReadAsList(r => InitTotalVolumeInfoEntity(new TotalVolumeInfoEntity(), r, field), query, date, (int)oper, (int)field);
        }


        private static TotalVolumeInfoEntity InitTotalVolumeInfoEntity(TotalVolumeInfoEntity t, IDataRecord r, SumField field)
        {
            t.CurId = r.GetValue<int>("cur_id");
            switch (field)
            {
                case SumField.DocSum:
                    t.AverageCourse = r.GetValue<decimal>("course_doc_sum");
                    t.Amount = r.GetValue<decimal>("doc_sum");
                    break;
                case SumField.Rest:
                    t.AverageCourse = r.GetValue<decimal>("course_rest");
                    t.Amount = r.GetValue<decimal>("rest");
                    break;
            }
            return t;
        }

        private enum SumField
        {
            DocSum = 0,
            Rest = 1
        }

        /// <summary>
        /// Создание сделки на основании заявки
        /// </summary>
        /// <param name="docSum">Сумма сделки</param>
        /// <returns></returns>
        public Deal CreateDeal(decimal docSum = 0)
        {
            if (Rest == 0)
                return null;

            var deal = new Deal
                       {
                           TenderId = Id,
                           CreateDate = DateTime.Now,
                           Course = CourseOrient,
                           UserId = WebSession.CurrentUser.Id,
                           CurId = CurId,
                           Operation = Operation,
                           OperSign = OperSign
                       };

            if (docSum == 0 || docSum > Rest)
            {
                deal.DocSum = Rest;
            }
            else
            {
                deal.DocSum = docSum;
            }
            Rest -= docSum;

            return deal;
        }

        
    }
}