using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportTypeSwitcher.Parsers;

namespace ReportTypeSwitcher.Reports
{
    class DailyPnl:IReport
    {
        readonly ArgsParserSequence _sequence = new ArgsParserSequence();

        public DailyPnl()
        {
            _sequence.AddParser(ReportArguments.Overwrite, new BoolParser());
            _sequence.AddParser(ReportArguments.Portfolios, new PortfoliosParser());
            _sequence.AddParser(ReportArguments.Date, new DailyPnlDateTimeParser());
        }
        public string ReportType { get { return "DAILYPNL"; } }
        public void Execute(string[] args)
        {
            Console.WriteLine(ReportType + "|" + new ReportArgs(_sequence.ParseArgs(args)));
        }
    }
}
