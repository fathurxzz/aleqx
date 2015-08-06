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
            _sequence.AddParser("overwrite", new BoolParser());
            _sequence.AddParser("portfolio", new StringParser());
            _sequence.AddParser("daterange", new BondAttributionDateRangeParser(new DateTimeParser()));
            _sequence.AddParser("histLag", new StringParser(new ArgsParserOptions<string> { DefaultValue = "6M" }));
            _sequence.AddParser("tenor", new StringParser(new ArgsParserOptions<string> { DefaultValue = "1D" }));
            _sequence.AddParser("region", new StringParser(new ArgsParserOptions<string> { DefaultValue = "All" }));

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
