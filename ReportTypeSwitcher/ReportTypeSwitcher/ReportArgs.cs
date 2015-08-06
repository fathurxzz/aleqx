using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportTypeSwitcher.Parsers;

namespace ReportTypeSwitcher
{
    class ReportArgs
    {
        public bool Overwrite { get; set; }
        public string Portfolio { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ReportArgs(IDictionary<string, object> parsedArgs)
        {
            Overwrite = (bool) parsedArgs["overwrite"];
            Portfolio = (string) parsedArgs["portfolio"];
            var range = (DateRange) parsedArgs["daterange"];
            StartDate = range.StartDate;
            EndDate = range.EndDate;
        }

        public override string ToString()
        {
            return String.Format("Overwrite: {0}, Portfolio: {1}, StartDate: {2}, EndDate: {3}", Overwrite, Portfolio,
                StartDate, EndDate);
        }
    }
}
