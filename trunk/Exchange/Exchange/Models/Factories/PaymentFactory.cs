using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models.Factories
{
    public class PaymentFactory
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

        public PaymentFactory(IDbConnection connection)
        {
            _conn = connection;
        }

        public bool Save(IEnumerable<Payment> payments)
        {
            using (var tran = _conn.BeginTransaction())
            {
                try
                {
                    foreach (var payment in payments)
                    {
                        Save(payment);
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _errorMessage = ex.Message;
                    return false;
                }
            }
        }

        public IEnumerable<Payment> GetPayments(DateTime date)
        {
            string query = "exec tx_getPaymentsByDate ?";
            return _conn.ReadAsList(r => Payment.InitPayment(new Payment(), r), query, date);
        }

        public IEnumerable<Payment> GetPayments(int orderId)
        {
            string query = "exec tx_getPaymentsByOrderId ?";
            return _conn.ReadAsList(r => Payment.InitPayment(new Payment(), r), query, orderId);
        }

        public IEnumerable<Payment> GetPayments(IEnumerable<int> paymentIds)
        {
            return paymentIds.Select(GetPayment).ToList();
        }

        public Payment GetPayment(int id)
        {
            string query = "exec tx_getPayment ?";
            return _conn.ReadAsList(r => Payment.InitPayment(new Payment(), r), query, id).FirstOrDefault();
        }

        private void Save(Payment p)
        {
            int id = Helper.GetId(_conn, "tx_payment");
            string query =
                "Insert into tx_payment(id,order_id,create_date,paid,cur_id,doc_sum,oper_id,deal_cur_id,course,ex_code,office_id,equiv,user_id,oper_sign)VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?)";

            _conn.ExecuteNonQuery(query,
                id,
                p.OrderId,
                p.CreateDate,
                p.Paid,
                p.CurId,
                p.DocSum.With(DbType.Decimal).With(18, 2),
                (int)p.Operation,
                p.DealCurId,
                p.Course.With(DbType.Decimal).With(15, 8),
                (int)p.ExCode,
                p.OfficeId,
                p.Equivalent.With(DbType.Decimal).With(18, 2),
                p.UserId,
                (int)p.OperSign
                );
        }

        private void Update(Payment p)
        {
            string query = "update tx_payment set paid=?, pay_date=?,os_file_name=? where id=?";
            _conn.ExecuteNonQuery(query, p.Paid, p.PayDate.With(DbType.DateTime),p.OsFileName, p.Id);
        }

        public bool Update(IEnumerable<Payment> payments)
        {
            using (var tran = _conn.BeginTransaction())
            {
                try
                {
                    foreach (var payment in payments)
                    {
                        Update(payment);
                    }
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _errorMessage = ex.Message;
                    return false;
                }
            }
        }

        public void Delete(int orderId)
        {
            string query = "delete from tx_payment where order_id=?";
            _conn.ExecuteNonQuery(query, orderId);
        }

        public void UnPaid(IEnumerable<int> paymentIds)
        {
            foreach (var paymentId in paymentIds)
            {
                UnPaid(paymentId);
            }
        }

        private void UnPaid(int id)
        {
            string query = "update tx_payment set pay_date=null, paid=0, os_file_name=null where id=?";
            _conn.ExecuteNonQuery(query, id);
        }
    }
}