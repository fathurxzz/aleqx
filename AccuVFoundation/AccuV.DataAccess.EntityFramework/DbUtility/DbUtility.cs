using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccuV.DataAccess.EntityFramework.DbUtility
{
    internal static class DbUtility
    {
        public static string GenerateProcedureCall(string procedureName, IEnumerable<SqlParameter> parameters)
        {
            StringBuilder builder = new StringBuilder(procedureName);
            foreach (var sqlParameter in parameters)
            {
                builder.AppendFormat(" @{0}{1}", sqlParameter.ParameterName,
                    sqlParameter.Direction == ParameterDirection.Output ? " out" : "");
            }
            return builder.ToString();
        }

        //public DataSet ExecStoredProcedure(string name, IEnumerable<DbParameter> parameters)
        //{
        //    var result = new DataSet();
        //    var factory = DbProviderFactories.GetFactory();
        //    using (var connection = factory.CreateConnection())
        //    using (var command = connection.CreateCommand())
        //    using (var adapter = factory.CreateDataAdapter())
        //    {
        //        connection.ConnectionString = ConnectionString.ConnectionString;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = name;
        //        command.Parameters.AddRange(parameters.ToArray());
        //        connection.Open();
        //        adapter.SelectCommand = command;
        //        adapter.Fill(result);
        //    }
        //    return result;
        //}

        //private string GetRowFieldAsString(DataRow row, DataColumn column)
        //{
        //    var field = row[column];
        //    if (field is DBNull || field == null)
        //    {
        //        return null;
        //    }

        //    if (field is DateTime)
        //    {
        //        return ((DateTime)field).ToString("MM/dd/yyyy");
        //    }
        //    return field.ToString();
        //}
    }
}
