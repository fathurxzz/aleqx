using System;
using System.Collections.Generic;
using System.Configuration;
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
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                    //"server=mysql302.1gb.ua;User Id=gbua_kulumu2013;password=3dd4af67sgh;Persist Security Info=True;database=gbua_kulumu2013";
            
            return connection;
        }

        
    }
}