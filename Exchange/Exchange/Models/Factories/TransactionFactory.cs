using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models.Factories
{
    public class TransactionFactory
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

        public TransactionFactory(IDbConnection connection)
        {
            _conn = connection;
        }

        private static IEnumerable<Transaction> GetTransactionsFromOdb(DateTime dateFrom, DateTime dateTo)
        {

            using (var odbConn = DbConnector.GetCustomConnection("ConnStrTrans"))
            {
                string query =
                    @"
                    select 
                        num_pay as tr_id,
                        date_odb as tr_date,
                        bank_id as podrid,
                        r030 as cur_id,
                        sum_pay_v as tr_sum,
                        detail as detail
                    from 
                        payment_paid (index idx_DATE_NUM) 
                    where 
                        date_odb  between ? and ? and 
                        account_c = '290079011' 
                        and account_d not like'6204%'";
                return odbConn.ReadAsList(r => Transaction.InitFromOdb(new Transaction(), r), query, dateFrom.Date,
                                           dateTo.Date);
            }
        }

        public IEnumerable<Transaction> GetTransactions(DateTime date)
        {
            return GetTransactions(date, date);
        }

        public IEnumerable<Transaction> GetTransactions(DateTime dateFrom, DateTime dateTo)
        {
            string query = "select * from tx_trans where tr_date between ? and ?";
            return _conn.ReadAsList(r => Transaction.Init(new Transaction(), r), query, dateFrom.Date, dateTo.Date);
        }

        public Transaction GetTransaction(int id)
        {
            string query = "select t1.*,(select max(t2.name) from tx_office t2 where convert(varchar,t2.podrid)=t1.podrid and t2.office_level='1') as office_name from tx_trans t1 where t1.id = ?";
            return _conn.ReadAsList(r => Transaction.Init(new Transaction(), r), query, id).First();
        }

        public bool LoadTransactions()
        {
            IEnumerable<Transaction> odbTransactions = GetTransactionsFromOdb(WebSession.DateFrom, WebSession.DateTo);
            IEnumerable<Transaction> ownTransactions = GetTransactions(WebSession.DateFrom, WebSession.DateTo);
            var exceptTransactions = odbTransactions.Where(o => !ownTransactions.Any(t => t.TrId == o.TrId && t.TrDate == o.TrDate));
            return InsertTransactions(exceptTransactions);
        }

        private bool InsertTransactions(IEnumerable<Transaction> transactions)
        {
            string query = "INSERT INTO tx_trans(id,tr_id,tr_date,cur_id,tr_sum,podrid,detail,kv_sum)VALUES(?,?,?,?,?,?,?,?)";
            foreach (var t in transactions)
            {
                using (var tran = _conn.BeginTransaction())
                {
                    try
                    {
                        int id = Helper.GetId(_conn, "tx_trans");
                        _conn.ExecuteNonQuery(query, id, t.TrId, t.TrDate, t.CurId, t.TrSum.With(DbType.Decimal).With(18, 2), t.Podrid, t.Detail, t.KvSum.With(DbType.Decimal).With(18, 2));
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        _errorMessage = Helper.GetResourceString("CannotInsertTransaction") + ex.Message;
                        return false;
                    }
                }
            }
            return true;
        }

        public bool Update(IEnumerable<Transaction> transactions)
        {
            return transactions.All(Update);
        }

        public bool Update(Transaction transaction)
        {
            try
            {
                string query = "update tx_trans set kv_date=?, kv_sum=?, deleted=?,tender_id=? where id=?";
                _conn.ExecuteNonQuery(
                    query, 
                    transaction.KvDate.With(DbType.DateTime), 
                    transaction.KvSum.With(DbType.Decimal).With(18,2), 
                    transaction.Deleted, 
                    transaction.TenderId.With(DbType.Int32), 
                    transaction.Id);
                return true;
            }
            catch (Exception ex)
            {
                _errorMessage = Helper.GetResourceString("CannotUpdateTransaction") + ": " + ex.Message;
                return false;
            }
        }


    }
}