using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportTypeSwitcher.Parsers;

namespace ReportTypeSwitcher.Reports
{
    class CoreAnalytics:IReport
    {
        readonly ArgsParserSequence _sequence = new ArgsParserSequence();

        public CoreAnalytics()
        {
            _sequence.AddParser(ReportArguments.Intex, new BoolParser());
            _sequence.AddParser(ReportArguments.Overwrite, new BoolParser());
            _sequence.AddParser(ReportArguments.Date, new CoreAnalyticsDateTimeParser());
        }
        public string ReportType { get { return "COREANALYTICS"; } }
        public void Execute(string[] args)
        {
            Console.WriteLine(ReportType + "|" + new ReportArgs(_sequence.ParseArgs(args)));
        }
    }
}
