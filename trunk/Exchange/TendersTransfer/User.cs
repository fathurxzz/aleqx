using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Exchange.Models.Helpers;

namespace TendersTransfer
{
    class User
    {
        public static IEnumerable<Exchange.Models.User> GetUsers(IDbConnection conn)
        {
            string query = "select t1.*, t2.podrid from tnd_user t1 join tnd_office t2 on t1.office_id=t2.office_id where user_id>6 and is_active=1";
            return conn.ReadAsList(r => InitUser(new Exchange.Models.User(), r), query);
        }

        private static Exchange.Models.User InitUser(Exchange.Models.User u, IDataRecord r)
        {
            u.Id = r.GetValue<int>("user_id");
            u.Name = r.GetValue<string>("user_name");
            u.TabNum = r.GetValue<string>("clock_num");
            u.UserCode = r.GetValue<string>("user_code");
            u.Podrid = r.GetValue<string>("podrid");
            return u;
        }


        

        public static void UpdateOffice(IDbConnection conn, IEnumerable<Exchange.Models.User> users)
        {

            string query = "select office_id from tx_office where podrid=? and office_level='1' and date_close is null";

            int oldOfficeId = 1;
            foreach (var user in users)
            {
                try
                {
                    user.OfficeId = conn.ExecuteScalar<int>(query, user.Podrid);
                    oldOfficeId = user.OfficeId;
                }
                catch
                {
                    user.OfficeId = oldOfficeId;
                }
                //Console.WriteLine(user.OfficeId);
            }
        }


        public static void CreateUser(IDbConnection conn, string userName, string tabNum, string userCode, int officeId, int id)
        {
            string query = "insert into tx_user(id,name,office_id,tab_num,parent_office_id,user_code)VALUES(?,?,?,?,?,?)";
            conn.ExecuteNonQuery(query, id, userName, officeId, Convert.ToInt32(tabNum), officeId, userCode);

        }
    }
}
