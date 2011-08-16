using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace Exchange.Models.Helpers
{
    public partial class Helper
    {
        public static string GetCurName(int curId, IDbConnection conn)
        {
            const string query = "SELECT cur_name FROM tnd_currency WHERE cur_id=?";
            var result = conn.ExecuteScalar<string>(query, curId);
            return result ?? "вся валюта";
        }
    }
}