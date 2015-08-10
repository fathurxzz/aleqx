using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportTypeSwitcher.Parsers;

namespace ReportTypeSwitcher.Reports
{
    class AssetMixHistory : IReport
    {
        readonly ArgsParserSequence _sequence = new ArgsParserSequence();
        public string ReportType { get { return "ASSETMIXHISTORY"; } }
        public AssetMixHistory()
        {
            _sequence.AddParser(ReportArguments.Overwrite, new BoolParser());
            _sequence.AddParser(ReportArguments.Portfolio, new StringParser());
            _sequence.AddParser(ReportArguments.Date, new AssetMixHistoryDateTimeParser());
        }
        public void Execute(string[] args)
        {
            Console.WriteLine(ReportType + "|" + new ReportArgs(_sequence.ParseArgs(args)));
        }
    }
}
