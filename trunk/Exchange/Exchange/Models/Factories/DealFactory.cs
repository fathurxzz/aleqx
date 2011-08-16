using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Helpers;

namespace Exchange.Models.Factories
{
    public class DealFactory
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

        public DealFactory(IDbConnection connection)
        {
            _conn = connection;
        }

        public Deal GetDeal(int id)
        {
            string query = "exec tx_getDeal ?";
            return _conn.ReadAsList(r=>new Deal(r), query, id).FirstOrDefault();
        }

        public IEnumerable<Deal> GetDeals(IEnumerable<int> ids)
        {
            return ids.Select(GetDeal).ToList();
        }

        public IEnumerable<Deal> GetDeals(DateTime date)
        {
            return GetDeals(date, date);
        }

        public IEnumerable<Deal> GetDeals(DateTime dateFrom, DateTime dateTo)
        {
            string query = "exec tx_getDeals ?,?";
            return _conn.ReadAsList(r=>new Deal(r), query, dateFrom, dateTo).ToList();
        }

        public IEnumerable<DealPresentation> GetDelasPresentation(DateTime dateFrom, DateTime dateTo)
        {
            string query = "exec tx_getDealsPresentation ?,?";
            return _conn.ReadAsList(r => new DealPresentation(r), query, dateFrom, dateTo).ToList();
        }

        public bool Update(Deal d)
        {
            using (var tran = _conn.BeginTransaction())
            {
                try
                {
                    string query = "update tx_deal set course_mor=? where id=?";
                    _conn.ExecuteNonQuery(query, d.CourseMor.With(DbType.Decimal), d.Id);
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    _errorMessage = string.Format("{0} №{1}: {2}", Helper.GetResourceString("AnErrorWhileUpdatingDeal"), d.Id, ex.Message);
                }
            }
            return false;
        }

        public bool Update(IEnumerable<Deal> deals)
        {
            return deals.All(Update);
        }
    }
}