using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Parsers
{
    struct DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    class DateRangeParser : ArgsParser<DateRange>
    {
        private readonly IArgsParser<DateTime> _dateParser;

        public DateRangeParser(IArgsParser<DateTime> dateParser)
        {
            _dateParser = dateParser;
        }

        public override int ArgsCount { get { return 2; } }

        public override DateRange ParseArgs(IList<string> args)
        {
            return new DateRange
            {
                StartDate = _dateParser.ParseArgs(args),
                EndDate = _dateParser.ParseArgs(args.Skip(1).ToList()),
            };
        }
    }
}
