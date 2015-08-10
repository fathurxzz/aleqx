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
        public Guid? Token { get; set; }
        public string Strategy { get; set; }
        public string SubStrategy { get; set; }
        public string Interval { get; set; }
        public int IncludeAccrual { get; set; }
        public bool Intex { get; set; }
        public IEnumerable<string> Portfolios { get; set; }

        public ReportArgs(IDictionary<ReportArguments, object> parsedArgs)
        {
            Overwrite = (bool)parsedArgs[ReportArguments.Overwrite];
            if (parsedArgs.ContainsKey(ReportArguments.Portfolio))
            {
                Portfolio = (string)parsedArgs[ReportArguments.Portfolio];
            }
            if (parsedArgs.ContainsKey(ReportArguments.DateRange))
            {
                var range = (DateRange)parsedArgs[ReportArguments.DateRange];
                StartDate = range.StartDate;
                EndDate = range.EndDate;
            }
            if (parsedArgs.ContainsKey(ReportArguments.Date))
            {
                EndDate = (DateTime)parsedArgs[ReportArguments.Date];
            }
            if (parsedArgs.ContainsKey(ReportArguments.HistLag))
            {
                HistLag = (string)parsedArgs[ReportArguments.HistLag];
            }
            if (parsedArgs.ContainsKey(ReportArguments.Tenor))
            {
                Tenor = (string)parsedArgs[ReportArguments.Tenor];
            }
            if (parsedArgs.ContainsKey(ReportArguments.Region))
            {
                Region = (string)parsedArgs[ReportArguments.Region];
            }
            if (parsedArgs.ContainsKey(ReportArguments.Token))
            {
                Token = (Guid?)parsedArgs[ReportArguments.Token];
            }
            if (parsedArgs.ContainsKey(ReportArguments.Strategy))
            {
                Strategy = (string)parsedArgs[ReportArguments.Strategy];
            }
            if (parsedArgs.ContainsKey(ReportArguments.SubStrategy))
            {
                SubStrategy = (string)parsedArgs[ReportArguments.SubStrategy];
            }
            if (parsedArgs.ContainsKey(ReportArguments.Interval))
            {
                Interval = (string)parsedArgs[ReportArguments.Interval];
            }
            if (parsedArgs.ContainsKey(ReportArguments.IncludeAccrual))
            {
                IncludeAccrual = (int)parsedArgs[ReportArguments.IncludeAccrual];
            }
            if (parsedArgs.ContainsKey(ReportArguments.Intex))
            {
                Intex = (bool)parsedArgs[ReportArguments.Intex];
            }
            if (parsedArgs.ContainsKey(ReportArguments.Portfolios))
            {
                Portfolios = (IEnumerable<string>)parsedArgs[ReportArguments.Portfolios];
            }

        }

        public override string ToString()
        {
            return
                String.Format("\r\nOverwrite: {0}\r\nPortfolio: {1}\r\nStartDate: {2}\r\nEndDate: {3}\r\nHistLag:{4}\r\nTenor:{5}\r\nRegion:{6}\r\nToken:{7}\r\nStrategy:{8}\r\nSubStrategy:{9}\r\nInterval:{10}\r\nIncludeAccrual:{11}\r\nIntex:{12}\r\nPortfolios:{13}",
                    Overwrite, Portfolio, StartDate, EndDate, HistLag, Tenor, Region, Token, Strategy, SubStrategy, Interval, IncludeAccrual, Intex, Portfolios.Aggregate((a, b) => a + ", " + b));
        }
    }
}
