using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace TendersTransfer
{
    static class Office
    {
        public static IEnumerable<Exchange.Models.Office> GetOffices(IDbConnection conn)
        {
            string query = "select * from dictionary..regdivision where tp in ('1','2')";
            return conn.ReadAsList(r => new Exchange.Models.Office
                                            {
                                                OfficeId = r.GetValue<int>("rd_id"),
                                                OfficeName = r.GetValue<string>("name"),
                                                DateClose = r.GetValue<DateTime?>("date_close"),
                                                OfficeLevel = r.GetValue<int>("tp"),
                                                Podrid = r.GetValue<string>("podrid")

                                            }, query);
        }

        public static void Update(Exchange.Models.Office office, IDbConnection conn)
        {
            var cnt = conn.ExecuteScalar<int>("select count(1) as cnt from tx_office where office_id=?", office.OfficeId);
            if (cnt > 0)
            {
                conn.ExecuteNonQuery(
                    "update tx_office set podrid=?, name=?, office_level=?, date_close=? where office_id=?",
                    office.Podrid,
                    office.OfficeName,
                    office.OfficeLevel.ToString(),
                    office.DateClose.With(DbType.DateTime),
                    office.OfficeId);
            }
            else
            {
                conn.ExecuteNonQuery("insert into tx_office(office_id,podrid,name,office_level,date_close)values(?,?,?,?,?)",
                    office.OfficeId,
                    office.Podrid,
                    office.OfficeName,
                    office.OfficeLevel,
                    office.DateClose.With(DbType.DateTime)
                    );
            }


        }
    }
}
