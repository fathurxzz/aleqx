using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace Kulumu.Helpers
{
    public static class DbHelper
    {
        public static IDbConnection Connection
        {
            get
            {
                var factory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
                return  Conn(factory);
            }
        }

        private static IDbConnection Conn(DbProviderFactory factory)
        {
            var connection = factory.CreateConnection();
            if (connection != null)
                connection.ConnectionString = "server=127.0.0.1;User Id=root;password=root;Persist Security Info=True;database=kulumu2013";
            return connection;
        }

        
    }
}