using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Exchange.Models.Helpers
{
    public static partial class Helper
    {
        public static int GetId(IDbConnection connection, string tableName)
        {
            using (var cmd = connection.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "exec f_TABLE_GENERATION_KEY ?, ?";
                    cmd.AddParameter(DbType.AnsiString, "table_name").Value = tableName;
                    cmd.AddParameter(DbType.Int32, ParameterDirection.Output, "key_id");
                    cmd.ExecuteNonQuery();
                    int keyId = Convert.ToInt32(cmd.GetParameter("key_id").Value);
                    return keyId;
                }
                catch (Exception ex)
                {
                    throw new Exception("Невозможно получить идентификатор из таблицы TABLE_GENERATION_KEY " + ex.Message);
                }
            }
        }
    }
}