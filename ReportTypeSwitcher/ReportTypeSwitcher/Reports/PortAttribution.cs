using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportTypeSwitcher.Parsers;

namespace ReportTypeSwitcher.Reports
{
    class PortAttribution : IReport
    {
        readonly ArgsParserSequence _sequence = new ArgsParserSequence();
        public PortAttribution()
        {
            _sequence.AddParser(ReportArguments.Overwrite, new BoolParser());
            _sequence.AddParser(ReportArguments.Portfolio, new StringParser());
            _sequence.AddParser(ReportArguments.DateRange, new PortAttributionDateRangeParser(new DateTimeParser()));
        }

        public string ReportType
        {
            get { return "PORTATTRIBUTION"; }
        }

        public void Execute(string[] args)
        {

        }
    }
}
