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
    /// Угода
    /// </summary>
    public class Deal : TenderDeal, ICurrencyFilter, IOperationFilter, IOperSignFilter
    {
        /// <summary>
        /// Ідентифікатор завки, з якої сформована угода
        /// </summary>
        public int TenderId { get; set; }

        /// <summary>
        /// Ідентифікатор розпорядження, в яке включена угода
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Гривневий еквівалент
        /// </summary>
        public decimal Equivalent { get; set; }

        /// <summary>
        /// Валютний ринок
        /// </summary>
        public ExchangeMarket ExCode { get; set; }

        /// <summary>
        /// Курс розрахунку з клієнтом
        /// </summary>
        public decimal? CourseClient { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public DealStatus Status { get; set; }

        /// <summary>
        /// Курс заявки
        /// </summary>
        public decimal TenderCourse { get; set; }

        /// <summary>
        /// Курс заявки розрахунковий
        /// </summary>
        public decimal TenderCourseOrient { get; set; }

        /// <summary>
        /// Курс MOR
        /// </summary>
        public decimal? CourseMor { get; set; }


        public decimal DeltaSum { get; set; }
        public bool Deleted { get; set; }


        public Deal()
        {

        }

        public Deal(IDataRecord r)
        {
            Id = r.GetValue<int>("id");
            TenderId = r.GetValue<int>("tender_id");
            OrderId = r.GetValue<int>("order_id");
            Status = r.GetValue<DealStatus>("status");
            DocSum = r.GetValue<decimal>("doc_sum");
            CreateDate = r.GetValue<DateTime>("create_date");
            Course = r.GetValue<decimal>("course");
            Equivalent = r.GetValue<decimal>("equiv");
            ExCode = r.GetValue<ExchangeMarket>("ex_code");
            CourseClient = r.GetValue<decimal?>("course_client");
            UserId = r.GetValue<int>("user_id");
            OfficeId = r.GetValue<int>("office_id");
            OfficeName = r.GetValue<string>("office_name");
            ParentOfficeId = r.GetValue<int>("parent_office_id");
            ParentOfficeName = r.GetValue<string>("parent_office_name");
            Operation = r.GetValue<Models.EOperation>("oper_id");
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
            Podrid = r.GetValue<string>("podrid");
            CourseMor = r.GetValue<decimal?>("course_mor");

            if (r.ContainsColumn("tender_course"))
                TenderCourse = r.GetValue<decimal>("tender_course");
            if (r.ContainsColumn("tender_course_orient"))
                TenderCourseOrient = r.GetValue<decimal>("tender_course_orient");
            
            OperName = Helper.GetResourceString(Operation.ToString());
            StatusName = Helper.GetResourceString(Status.ToString());
            OperSignName = Helper.GetResourceString(OperSign.ToString());

            CurDef = r.GetValue<string>("cur_def");
            CurName = r.GetValue<string>("cur_name");
        }
    }
}