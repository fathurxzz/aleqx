using System.Configuration;
using System.Data;
using Sybase.Data.AseClient;

namespace Exchange.Models.Helpers
{
    public static class DbConnector
    {
        public static IDbConnection Connection
        {
            get
            {
                string key = ConfigurationManager.AppSettings["connect_to"];

                if (key == null)
                    throw new ConfigurationErrorsException(
                                "В файле конфигурации не указан ключ строки соеденения с базой данных (connect_to)");
                var conn = new AseConnection
                {
                    ConnectionString = ConfigurationManager.ConnectionStrings[key].ConnectionString,
                    NamedParameters = false
                };
                conn.Open();
                return conn;
            }
        }

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