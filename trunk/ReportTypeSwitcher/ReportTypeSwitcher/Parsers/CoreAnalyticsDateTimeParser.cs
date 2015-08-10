using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Parsers
{
    class CoreAnalyticsDateTimeParser : ArgsParser<DateTime>
    {
        private int _argsCount = 1;
        public override int ArgsCount { get { return _argsCount; } }
        public override DateTime ParseArgs(IList<string> args)
        {
            if (args.Count == 0)
            {
                _argsCount = 0;
            }

            return args.Count > 0
                ? "TODAY".Equals(args[0], StringComparison.OrdinalIgnoreCase)
                ? DateTime.Today
                : Helpers.DatesHelper.ConvertToDate(args[0])
                : DateTime.Today.AddDays(-1);
        }
    }
}
