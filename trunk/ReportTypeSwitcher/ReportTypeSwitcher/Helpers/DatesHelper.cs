using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Helpers
{
    class DatesHelper
    {
        public static DateTime ConvertToDate(string s)
        {
            if (s == null)
            {
                throw new System.ArgumentException("unknown date " + s);
            }

            DateTime dt;
            if (DateTime.TryParse(s, out dt)) return dt;

            DateTime baseDt = DateTime.Today;
            switch (s)
            {
                case "TODAY":
                    return baseDt;
                case "PME":
                    return new DateTime(baseDt.Year, baseDt.Month, 1).AddDays(-1);
                case "PREV":
                    return baseDt.AddDays(-1);
                default:
                    throw new System.ArgumentException("unknown date " + s);
            }
        }
    }
}
