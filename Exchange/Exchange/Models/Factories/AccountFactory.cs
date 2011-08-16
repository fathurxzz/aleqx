using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models.Factories
{
    public class AccountFactory
    {
        private readonly IDbConnection _conn;

        public AccountFactory(IDbConnection connection)
        {
            _conn = connection;
        }

        public IList<Account> GetAccounts(int officeId)
        {
            if (officeId == 0)
                return GetAccounts();
            string query = "select t1.*,t2.name as office_name,t2.podrid from tx_accounts t1 join tx_office t2 on t1.office_id=t2.office_id where t1.office_id=?";
            return _conn.ReadAsList(r => InitAccount(new Account(), r), query, officeId);
        }

        public IEnumerable<Account> GetAccounts(int officeId,int operId, int curId, int operSign)
        {
            IList<Account> result = GetAccounts(officeId);
            return result.Where(a => a.CurId == curId && a.OperId==operId && a.OperSign == operSign).ToList();
        }

        public IList<Account> GetAccounts()
        {
            string query = "select t1.*,t2.name as office_name,t2.podrid from tx_accounts t1 join tx_office t2 on t1.office_id=t2.office_id";
            return _conn.ReadAsList(r => InitAccount(new Account(), r), query);
        }

        public Account GetAccount(int id)
        {
            string query = "select t1.*,t2.name as office_name,t2.podrid from tx_accounts t1 join tx_office t2 on t1.office_id=t2.office_id where t1.id=?";
            return _conn.ReadAsList(r => InitAccount(new Account(), r), query, id).First();
        }

        private static Account InitAccount(Account a, IDataRecord r)
        {
            a.Id = r.GetValue<int>("id");
            a.OfficeId = r.GetValue<int>("office_id");
            a.OperId = r.GetValue<int>("oper_id");
            a.CurId = r.GetValue<int>("cur_id");
            a.Nls = r.GetValue<string>("nls");
            a.OperSign = r.GetValue<int>("oper_sign");
            a.OfficeName = r.GetValue<string>("office_name");
            a.Podrid = r.GetValue<string>("podrid");
            return a;
        }

        public void Save(Account a)
        {
            using (IDbTransaction tran = _conn.BeginTransaction())
            {
                try
                {
                    int accId = Helper.GetId(_conn, "tx_accounts");
                    string query = "insert into tx_accounts(id,office_id,oper_id,cur_id,nls,oper_sign)values(?,?,?,?,?,?)";
                    _conn.ExecuteNonQuery(query, accId, a.OfficeId, a.OperId, a.CurId, a.Nls, a.OperSign);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Delete(Account account)
        {
            string query = "delete from tx_accounts where id=?";
            _conn.ExecuteNonQuery(query, account.Id);
        }
    }
}