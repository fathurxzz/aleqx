using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportTypeSwitcher.Parsers;

namespace ReportTypeSwitcher.Reports
{
    class BondAttribution :  IReport
    {
        readonly ArgsParserSequence _sequence = new ArgsParserSequence();

        public BondAttribution()
        {
            _sequence.AddParser(ReportArguments.Overwrite, new BoolParser());
            _sequence.AddParser(ReportArguments.Portfolio, new StringParser());
            _sequence.AddParser(ReportArguments.DateRange, new BondAttributionDateRangeParser(new DateTimeParser()));
            _sequence.AddParser(ReportArguments.HistLag, new StringParser(new ArgsParserOptions<string> { DefaultValue = "6M" }));
            _sequence.AddParser(ReportArguments.Tenor, new StringParser(new ArgsParserOptions<string> { DefaultValue = "1D" }));
            _sequence.AddParser(ReportArguments.Region, new StringParser(new ArgsParserOptions<string> { DefaultValue = "All" }));

        }

        public string ReportType
        {
            get { return "BONDATTRIBUTION"; }
        }

        public void Execute(string[] args)
        {
            Console.WriteLine(ReportType + "|" + new ReportArgs(_sequence.ParseArgs(args)));
        }
    }
}
