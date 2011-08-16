using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Exchange.Models
{
    public class Office
    {
        public int OfficeId { get;  set; }
        public string Podrid { get;  set; }
        public string OfficeName { get;  set; }
        public int OfficeLevel { get;  set; }
        public DateTime? DateClose { get;  set; }

        /// <summary>
        /// Повертає список відділень в залежності від заданих параметрів
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="showClosed">Відображати закриті відділення</param>
        /// <param name="showNonbalance">Відображати безбалансові відділення</param>
        /// <param name="excludeOfficeIds">Ідентифікатори відділень, які непотрібно включати до списку</param>
        /// <returns></returns>
        public static IList<Office> GetOffices(IDbConnection connection, bool showClosed = false, bool showNonbalance = false, params int[] excludeOfficeIds)
        {
            IList<Office> result = GetOfficeList(connection);
            
            if (excludeOfficeIds!=null && excludeOfficeIds.Length > 0)
                result = result.Where(o => !o.OfficeId.In(excludeOfficeIds)).ToList();

            if (!showNonbalance)
                result = result.Where(o => o.OfficeLevel.In(new[] { 0, 1 })).ToList();

            if (!showClosed)
                result = result.Where(o => !o.DateClose.HasValue || (o.DateClose.Value.Date >= DateTime.Now.Date)).ToList();

            return result;
        }



        private static IList<Office> GetOfficeList(IDbConnection connection)
        {
            string query = "select * from tx_office order by podrid,office_level,date_close";
            return connection.ReadAsList(r => InitOffice(new Office(), r), query);
        }


        private static Office InitOffice(Office o, IDataRecord r)
        {
            o.OfficeId = r.GetValue<int>("office_id");
            o.Podrid = r.GetValue<string>("podrid");
            o.OfficeName = r.GetValue<string>("name");
            o.OfficeLevel = r.GetValue<int>("office_level");
            o.DateClose = r.GetValue<DateTime?>("date_close");
            return o;
        }
    }
}