using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Exchange.Models
{

    public class UserCurrency
    {
        /// <summary>
        /// Код валюти
        /// </summary>
        public int CurId { get; set; }

        /// <summary>
        /// Ознака дозволу обробки даної валюти
        /// </summary>
        public bool AllowedProcessing { get; set; }



        public static bool CheckForView(IEnumerable<UserCurrency> currencies, int curId)
        {
            return currencies.Any(currency => currency.CurId == curId);
        }

        public static bool CheckForProcessing(IEnumerable<UserCurrency> currencies, int curId)
        {
            return currencies.Any(currency => currency.CurId == curId && currency.AllowedProcessing);
        }
    }
}