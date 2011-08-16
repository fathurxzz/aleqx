using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exchange.Models.Helpers
{
    public static partial class Helper
    {
        public static decimal Round(decimal d,int decimals)
        {
            return Math.Round(d, decimals, MidpointRounding.AwayFromZero);
        }
    }
}