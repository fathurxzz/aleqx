using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportTypeSwitcher.Parsers;

namespace ReportTypeSwitcher.Reports
{
    class AssetMixDetail : IReport
    {
        readonly ArgsParserSequence _sequence = new ArgsParserSequence();

        public AssetMixDetail()
        {
            _sequence.AddParser(ReportArguments.Overwrite, new BoolParser());
            _sequence.AddParser(ReportArguments.Portfolio, new StringParser());
            _sequence.AddParser(ReportArguments.Interval, new StringParser(new ArgsParserOptions<string> { AcceptedValues = new[] { "d", "w", "m" } }));
            _sequence.AddParser(ReportArguments.Date, new AssetMixDetailDateTimeParser());
            _sequence.AddParser(ReportArguments.IncludeAccrual, new IntParser(new ArgsParserOptions<int>{DefaultValue = 0}));
        }

        public string ReportType { get { return "ASSETMIXDETAIL"; } }

        public void Execute(string[] args)
        {
            Console.WriteLine(ReportType + "|" + new ReportArgs(_sequence.ParseArgs(args)));
        }
    }
}
