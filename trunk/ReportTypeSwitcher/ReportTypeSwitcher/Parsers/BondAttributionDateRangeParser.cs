using System;
using System.Collections.Generic;
using System.Linq;
using ReportTypeSwitcher.Helpers;

namespace ReportTypeSwitcher.Parsers
{
    class BondAttributionDateRangeParser : ArgsParser<DateRange>
    {
        private int _argsCount = 2;

        private readonly IArgsParser<DateTime> _dateParser;
        public BondAttributionDateRangeParser(IArgsParser<DateTime> dateParser)
        {
            _dateParser = dateParser;
        }

        public override int ArgsCount { get { return _argsCount; } }

        public override DateRange ParseArgs(IList<string> args)
        {
            _argsCount = args.Count > 1 ? 2 : args.Count;

            var endDate = args.Count == 0 ? DateTime.Today.AddDays(-1) : DatesHelper.ConvertToDate(args[0]);
            var startDate = args.Count > 1 ? _dateParser.ParseArgs(args.Skip(1).ToList()) : new DateTime(endDate.Year, endDate.Month, 1);

            return new DateRange
            {
                StartDate = startDate,
                EndDate = endDate
            };
        }
    }
}
