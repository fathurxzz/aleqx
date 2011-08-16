using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using Sybase.Data.AseClient;

namespace TendersTransfer
{
    public static class DbConnector
    {
        public static IDbConnection GetCustomConnection(string connectionStringKey)
        {
            var conn = new AseConnection
            {
                ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString,
                NamedParameters = false
            };
            conn.Open();
            return conn;
        }
    }
}
