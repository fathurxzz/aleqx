using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Factories;
using Exchange.Models.Helpers;

namespace Exchange.Models
{
    public class Transaction : ICurrencyFilter
    {
        public static string ErrorMessage { get { return _errorMessage; } }
        private static string _errorMessage;

        public int Id { get; set; }
        public int TrId { get; set; }
        public DateTime TrDate { get; set; }
        public int CurId { get; set; }
        public string CurDef { get; set; }
        public string CurName { get; set; }
        public decimal TrSum { get; set; }
        public string Podrid { get; set; }
        public string OfficeName { get; set; }
        public string Detail { get; set; }
        public decimal KvSum { get; set; }
        public int? TenderId { get; set; }
        public DateTime? KvDate { get; set; }
        public bool Deleted { get; set; }
        


        public override string ToString()
        {
            return "Id: " + Id;
        }


        public static Transaction InitFromOdb(Transaction t, IDataRecord r)
        {
            t.TrId = r.GetValue<int>("tr_id");
            t.TrDate = r.GetValue<DateTime>("tr_date");
            t.CurId = r.GetValue<int>("cur_id");
            t.Detail = r.GetValue<string>("detail");
            t.TrSum = t.KvSum = r.GetValue<decimal>("tr_sum");
            t.Podrid = r.GetValue<string>("podrid");
            return t;
        }
        
        public static Transaction Init(Transaction t, IDataRecord r)
        {
            t.TrId = r.GetValue<int>("tr_id");
            t.TrDate = r.GetValue<DateTime>("tr_date");
            t.CurId = r.GetValue<int>("cur_id");
            t.Detail = r.GetValue<string>("detail");
            t.TrSum = r.GetValue<decimal>("tr_sum");
            t.KvSum = r.GetValue<decimal>("kv_sum");
            t.Podrid = r.GetValue<string>("podrid");

            if (r.ContainsColumn("deleted"))
                t.Deleted = r.GetValue<bool>("deleted");

            if (r.ContainsColumn("office_name"))
                t.OfficeName = r.GetValue("office_name", string.Empty);
            if (r.ContainsColumn("id"))
                t.Id = r.GetValue<int>("id");
            if (r.ContainsColumn("tender_id"))
                t.TenderId = r.GetValue<int?>("tender_id");
            if (r.ContainsColumn("kv_date"))
                t.KvDate = r.GetValue<DateTime?>("kv_date");
            t.CurDef = r.ContainsColumn("cur_def") ? r.GetValue("cur_def", string.Empty) : string.Empty;
            t.CurName = r.ContainsColumn("cur_name") ? r.GetValue("cur_name", string.Empty) : string.Empty;

            return t;
        }


        public static void LoadTransactions(IDbConnection conn)
        {
            var trFactory = new TransactionFactory(conn);
            if (!trFactory.LoadTransactions())
            {
                _errorMessage = trFactory.ErrorMessage;
            }
        }

        public static bool Matching(out int matchedTransactionsCount)
        {
            using (var conn = DbConnector.Connection)
            {
                return Matching(out matchedTransactionsCount, conn);
            }
        }

        public static bool Matching(out int matchedTransactionsCount, IDbConnection conn)
        {
            var trFactory = new TransactionFactory(conn);
            var trList = trFactory.GetTransactions(WebSession.DateFrom, WebSession.DateTo).ToList();
            return Matching(conn, trList.Where(t => !t.TenderId.HasValue), out matchedTransactionsCount);
        }

        public static bool Matching(IDbConnection conn, IEnumerable<Transaction> transactions, out int matchedTransactionsCount)
        {
            matchedTransactionsCount = 0;
            var trFactory = new TransactionFactory(conn);
            var tFactory = new TenderFactory(conn);
            var tenders = tFactory.GetTenders(WebSession.DateFrom, WebSession.DateTo).Where(t => t.Operation == EOperation.Sell && !t.KvDate.HasValue&&t.Status==TenderStatus.Sent).ToList();

            matchedTransactionsCount = Matching(transactions.Where(t => !t.KvDate.HasValue), tenders.Where(t => !t.KvDate.HasValue && !t.ExecuteSign));

            using (var tran = conn.BeginTransaction())
            {
                if (!tFactory.UpdateMatched(tenders))
                {
                    _errorMessage = tFactory.ErrorMessage;
                    tran.Rollback();
                    return false;
                }

                if (!trFactory.Update(transactions))
                {
                    _errorMessage = tFactory.ErrorMessage;
                    tran.Rollback();
                    return false;
                }
                tran.Commit();
                return true;
            }
            
        }


        private static int Matching(IEnumerable<Transaction> transactions, IEnumerable<Tender> tenders)
        {
            int cnt = 0;
            foreach (var tender in tenders)
            {
                foreach (var transaction in transactions)
                {
                    if (tender.KvDate.HasValue || transaction.TenderId.HasValue) continue;
                    if (!IsMatch(transaction, tender)) continue;

                    DateTime currentDate = DateTime.Now;
                    tender.KvDate = currentDate;
                    tender.ExecuteSign = true;
                    transaction.KvDate = currentDate;
                    transaction.TenderId = tender.Id;
                    cnt++;
                }
            }
            return cnt;
        }

        private static bool IsMatch(Transaction transaction, Tender tender)
        {
            return tender.CurId == transaction.CurId &&
                   tender.DocSum == transaction.KvSum &&
                   tender.Podrid == transaction.Podrid;
        }


    }
}