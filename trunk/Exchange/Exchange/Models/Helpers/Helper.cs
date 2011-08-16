using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Helpers
{
    public partial class Helper
    {
        private static bool Match(this string sourse, string other)
        {
            return sourse.Replace(",", ".") == other.Replace(",", ".");
        }

        public static string GetExchangeMarketName(ExchangeMarket exchangeMarket)
        {
            switch (exchangeMarket)
            {
                case ExchangeMarket.VVR:
                    return "Внутрішній валютний ринок";
                case ExchangeMarket.MVRU:
                    return "іжбанківський валютний ринок України";
                default:
                    return string.Empty;
            }
        }

        public static string GetExchangeMarketNameF(ExchangeMarket exchangeMarket)
        {
            switch (exchangeMarket)
            {
                case ExchangeMarket.VVR:
                    return "внутрішньому валютному ринку";
                case ExchangeMarket.MVRU:
                    return "міжбанківському валютному ринку України";
                default:
                    return string.Empty;
            }
        }
    }
}