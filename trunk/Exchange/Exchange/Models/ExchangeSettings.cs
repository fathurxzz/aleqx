using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Data;
using Exchange.Models.Helpers;

namespace Exchange.Models
{
    public class ExchangeSettings
    {
        public int TransactionMatchingInterval { get; set; }
        public bool CheckingCourseMor { get; set; }
        public DateTime CheckingCourseMorTime { get; set; }

        public static ExchangeSettings LoadSettings()
        {
            using (var conn = DbConnector.Connection)
            {
                var result = new ExchangeSettings();
                string query = "select setting_key,setting_value from tx_settings where user_id=?";
                using (var r = conn.ExecuteReader(query, WebSession.CurrentUser.Id))
                    while (r.Read())
                    {
                        var property = typeof(ExchangeSettings).GetProperty(r.GetValue<string>("setting_key"));


                        if (property != null)
                            property.SetValue(result, Convert.ChangeType(r.GetValue<string>("setting_value"), property.PropertyType),
                                null);
                        //typeof(ExchangeSettings)
                        //    .InvokeMember(r.GetValue<string>("key"), BindingFlags.Instance | BindingFlags.SetProperty,
                        //        null, result, new object[] {r.GetValue<string>("value")});
                    }
                return result;
            }
        }

        public static bool SaveSettings(ExchangeSettings exchangeSettings, out string errorMessage)
        {
            try
            {
                errorMessage = string.Empty;
                var properties = typeof(ExchangeSettings).GetProperties();
                using (var conn = DbConnector.Connection)
                {
                    foreach (PropertyInfo property in properties)
                    {
                        conn.ExecuteProcedure("tx_update_settings",
                            WebSession.CurrentUser.Id,
                            property.Name,
                            property.GetValue(exchangeSettings, null).ToString());
                    }
                }
                WebSession.ExchangeSettings = exchangeSettings;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                
            }
            return false;
        }
    }
}