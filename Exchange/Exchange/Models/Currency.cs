using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Exchange.Models.Entities;
namespace Exchange.Models
{
    public class Currency : IEquatable<Currency>,ICurrencyFilter
    {
        public int CurId { get; set; }
        public string CurName { get; set; }
        public string CurDef { get; set; }
        public bool Selected { get; set; }

        public static IList<Currency> GetAllCurrenciesAccordingAccounts(IDbConnection conn)
        {
            string query = @"select distinct t1.cur_id, t2.cur_def, t2.cur_name from tx_accounts t1
            join tx_currency t2 on t1.cur_id=t2.cur_id  where t1.cur_id<>980";
            return conn.ReadAsList(r => new Currency { CurId = r.GetValue<int>("cur_id"), CurDef = r.GetValue<string>("cur_def"), CurName = r.GetValue<string>("cur_name") }, query);
        }
        
        public static IEnumerable<Currency> GetAllCurrencies(IDbConnection conn)
        {
            string query = "select * from tx_currency";
            return conn.ReadAsList(r => new Currency { CurId = r.GetValue<int>("cur_id"), CurDef = r.GetValue<string>("cur_def"), CurName = r.GetValue<string>("cur_name") }, query);
        }

        public bool Equals(Currency other)
        {
            // Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            // Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            // Check whether the currencies' prEnum.Operationties are equal.
            return CurId.Equals(other.CurId) && CurDef.Equals(other.CurDef) && CurName.Equals(other.CurName);
        }

        public override int GetHashCode()
        {
            // Get the hash code for the CurId field.
            int hashCurrencyCurId = CurId.GetHashCode();

            // Get the hash code for the CurDef field if it is not null.
            int hashCurrencyCurDef = CurDef == null ? 0 : CurId.GetHashCode();

            // Get the hash code for the CurName field if it is not null.
            int hashCurrencyCurName = CurName == null ? 0 : CurName.GetHashCode();

            // Calculate the hash code for the currency.
            return hashCurrencyCurId ^ hashCurrencyCurDef ^ hashCurrencyCurName;
        }

    }
   

}