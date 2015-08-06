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
        public string HistLag { get; set; }
        public string Tenor { get; set; }
        public string Region { get; set; }

        public ReportArgs(IDictionary<string, object> parsedArgs)
        {
            Overwrite = (bool) parsedArgs["overwrite"];
            Portfolio = (string) parsedArgs["portfolio"];
            var range = (DateRange) parsedArgs["daterange"];
            StartDate = range.StartDate;
            EndDate = range.EndDate;
            HistLag = (string)parsedArgs["histLag"];
            Tenor = (string)parsedArgs["tenor"];
            Region = (string)parsedArgs["region"];
        }

        public override string ToString()
        {
            return
                String.Format(
                    "Overwrite: {0}, Portfolio: {1}, StartDate: {2}, EndDate: {3}, HistLag:{4}, Tenor:{5}, Region:{6}",
                    Overwrite, Portfolio, StartDate, EndDate, HistLag, Tenor, Region);
        }
    }
}
