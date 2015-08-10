using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportTypeSwitcher.Parsers;

namespace ReportTypeSwitcher.Reports
{
    class AssetMix : IReport
    {
        readonly ArgsParserSequence _sequence = new ArgsParserSequence();

        public AssetMix()
        {
            _sequence.AddParser(ReportArguments.Overwrite, new BoolParser());
            _sequence.AddParser(ReportArguments.Portfolio, new StringParser());
            _sequence.AddParser(ReportArguments.Date, new AssetMixDateTimeParser());
            _sequence.AddParser(ReportArguments.Strategy, new StringParser(new ArgsParserOptions<string> { DefaultValue = "" }));
            _sequence.AddParser(ReportArguments.SubStrategy, new StringParser(new ArgsParserOptions<string> { DefaultValue = "" }));
        }

        public string ReportType { get { return "ASSETMIX"; } }
        public void Execute(string[] args)
        {
            Console.WriteLine(ReportType + "|" + new ReportArgs(_sequence.ParseArgs(args)));
        }
    }
}
