using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Exchange.Models.Helpers;

namespace Exchange.Models.Factories
{
    public class TenderFactory
    {
        private readonly IDbConnection _conn;
        private string _errorMessage;

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
        }

        public TenderFactory(IDbConnection connection)
        {
            _conn = connection;
        }

        public int[] GetCurrencyIds(DateTime date)
        {
            string query = "select distinct(t1.cur_id) from tx_tender_deal t1 join tx_tender t2 on t1.tender_id=t2.id where datediff(day,t2.tndr_date,?)=0";
            return _conn.ReadAsList(r => r.GetValue<int>(0), query, date).ToArray();
        }

        public Tender GetTender(int id)
        {
            string query = "exec tx_getTender ?";
            return _conn.ReadAsList(r => new Tender(r), query, id).FirstOrDefault();
        }


        public IEnumerable<Tender> GetTendersByModerator(DateTime dateFrom, DateTime dateTo, int moderatorId)
        {
            string query = "exec tx_getTendersByModerator ?,?,?";
            return _conn.ReadAsList(r => new Tender(r), query, dateFrom, dateTo, moderatorId).ToList();
        }


        /*
        public TenderPresentation GetTenderPresentation(int id)
        {
            string query = "exec tx_getTender ?";
            return _conn.ReadAsList(r => new TenderPresentation(r), query, id).FirstOrDefault();
        }
        */


        public IEnumerable<Tender> GetTenders(DateTime dateFrom, DateTime dateTo)
        {
            string query = "exec tx_getTenders ?,?";
            return _conn.ReadAsList(r => new Tender(r), query, dateFrom, dateTo).ToList();
        }

        public IEnumerable<Tender> GetTenders(DateTime date)
        {
            return GetTenders(date, date);
        }

        public List<Tender> GetTenders(IEnumerable<int> ids)
        {
            return ids.Select(GetTender).ToList();
        }



        private bool Update(Tender t)
        {
            using (var tran = _conn.BeginTransaction())
            {
                try
                {
                    string query =
                        @"
                    UPDATE tx_tender 
                    SET 
                    
                    user_id=?,
                    status=?,
                    init_sum=?,
                    doc_sum=?,
                    rest=?,
                    course=?,
                    course_orient=?,
                    course_mor_sign=?,
                    create_date=?,
                    send_date=?,
                    tndr_date=?,
                    sell_type=?,
                    info_from_branch=?,
                    info_for_branch=?,
                    cancel_info=?,
                    execute_sign=?,
                    include_in_total=?,
                    is_labeled=?,
                    purpose=?,
                    auction=?,
                    kv_date=?,
                    course_client_recommend=?
                    WHERE id=?";

                    _conn.ExecuteNonQuery(query,

                        t.UserId,
                        (int)t.Status,
                        t.InitSum,
                        t.DocSum,
                        t.Rest,
                        t.Course,
                        t.CourseOrient,
                        t.CourseMorSign,
                        t.CreateDate,
                        t.SendDate.With(DbType.DateTime),
                        t.TenderDate.With(DbType.Date),
                        t.SellType.With(DbType.Int16),
                        t.InfoFromBranch,
                        t.InfoForBranch,
                        t.CancelInfo,
                        Convert.ToInt32(t.ExecuteSign),
                        Convert.ToInt32(t.IncludeInTotal),
                        Convert.ToInt32(t.IsLabeled),
                        t.Purpose,
                        Convert.ToInt32(t.Auction),
                        t.KvDate.With(DbType.DateTime),
                        t.CourseClientRecommend.With(DbType.Decimal),
                        t.Id);
                    query = @"update tx_tender_deal set 
                                office_id=?,
                                parent_office_id=?,
                                oper_id=?,
                                cur_id=?,
                                nls=?,
                                oper_sign=?,
                                commission=?,
                                extra_commission_sum=?,
                                client_code=?,
                                client_name=?,
                                client_subj_id=?,
                                client_cntr_code=?,
                                client_podrid_uah=?,
                                client_podrid_curr=?,
                                client_nls_curr=?,
                                client_nls_uah=?
                                where tender_id=?";
                    _conn.ExecuteNonQuery(query,
                                          t.OfficeId,
                                          t.ParentOfficeId,
                                          (int)t.Operation,
                                          t.CurId,
                                          t.Nls,
                                          (int)t.OperSign,
                                          t.Commission,
                                          t.ExtraCommissionSum.With(DbType.Decimal),
                                          t.ClientCode,
                                          t.ClientName,
                                          t.ClientSubjId.With(DbType.Int32),
                                          t.ClientCntrCode.With(DbType.Int32),
                                          t.ClientUahPodrid,
                                          t.ClientUahNls,
                                          t.ClientCurrPodrid,
                                          t.ClientCurrNls,
                                          t.Id);

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _errorMessage = string.Format("{0} №{1}: {2}", Helper.GetResourceString("AnErrorWhileUpdatingTender"), t.Id, ex.Message);
                }
            }
            return false;
        }

        public bool Save(Tender t)
        {
            if (t.Id == 0)
                return Insert(t);
            return t.Status == TenderStatus.ReadyForDelete ? Delete(t) : Update(t);
        }


        public bool UpdateMatched(IEnumerable<Tender> tenders)
        {
            string query = "Update tx_tender set kv_date=?,execute_sign=? where id=?";
            try
            {
                foreach (var tender in tenders.Where(t => t.KvDate.HasValue))
                {
                    _conn.ExecuteNonQuery(query, tender.KvDate, tender.ExecuteSign, tender.Id);
                }
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = Helper.GetResourceString("CannotUpdateTenders") + " " + ex.Message;
                return false;
            }
        }


        /*
        public bool Save(IEnumerable<Tender> tenders)
        {

        }
        */

        private bool Delete(Tender t)
        {
            using (var tran = _conn.BeginTransaction())
            {
                try
                {
                    string query = "delete from tx_tender_deal where tender_id=?";
                    _conn.ExecuteNonQuery(query, t.Id);
                    query = "delete from tx_tender where id=?";
                    _conn.ExecuteNonQuery(query, t.Id);
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _errorMessage = string.Format("{0} №{1}: {2}", Helper.GetResourceString("AnErrorWhileDeletingTender"), t.Id, ex.Message);
                }
            }
            return false;
        }

        private bool Insert(Tender t)
        {
            using (var tran = _conn.BeginTransaction())
            {
                try
                {
                    int id = Helper.GetId(_conn, "tx_tender");
                    string query = "insert into tx_tender(id,user_id,status,init_sum,doc_sum,rest,course,course_orient,course_mor_sign,create_date,send_date,tndr_date,sell_type,info_from_branch,info_for_branch,cancel_info,execute_sign,include_in_total,is_labeled,purpose,auction,kv_date,course_client_recommend)values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                    _conn.ExecuteNonQuery(
                        query, id,
                        
                        t.UserId,
                        (int)t.Status,
                        t.InitSum,
                        t.DocSum,
                        t.Rest,
                        t.Course,
                        t.CourseOrient,
                        t.CourseMorSign,
                        t.CreateDate,
                        t.SendDate.With(DbType.DateTime),
                        t.TenderDate.With(DbType.Date),
                        t.SellType.With(DbType.Int16),
                        t.InfoFromBranch,
                        t.InfoForBranch,
                        t.CancelInfo,
                        Convert.ToInt32(t.ExecuteSign),
                        Convert.ToInt32(t.IncludeInTotal),
                        Convert.ToInt32(t.IsLabeled),
                        t.Purpose,
                        Convert.ToInt32(t.Auction),
                        t.KvDate.With(DbType.DateTime),
                        t.CourseClientRecommend.With(DbType.Decimal));


                    query = "insert into tx_tender_deal(tender_id,office_id,parent_office_id,oper_id,cur_id,nls,oper_sign,commission,extra_commission_sum,client_code,client_name,client_subj_id,client_cntr_code,client_podrid_uah,client_podrid_curr,client_nls_curr,client_nls_uah)values(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
                    _conn.ExecuteNonQuery(
                        query,
                        id,
                        t.OfficeId,
                        t.ParentOfficeId,
                        (int)t.Operation,
                        t.CurId,
                        t.Nls,
                        (int)t.OperSign,
                        t.Commission,
                        t.ExtraCommissionSum.With(DbType.Decimal),
                        t.ClientCode,
                        t.ClientName,
                        t.ClientSubjId.With(DbType.Int32),
                        t.ClientCntrCode.With(DbType.Int32),
                        t.ClientUahPodrid,
                        t.ClientUahNls,
                        t.ClientCurrPodrid,
                        t.ClientCurrNls);

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _errorMessage = string.Format("{0}: {1}", Helper.GetResourceString("AnErrorWhileInsertingTender"), ex.Message);
                }
            }
            return false;
        }
    }
}