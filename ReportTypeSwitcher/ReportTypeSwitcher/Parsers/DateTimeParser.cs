using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Parsers
{
    class DateTimeParser:ArgsParser<DateTime>
    {
        public override DateTime ParseArgs(IList<string> args)
        {
            var str = args.First();
            return DateTime.Parse(str);
        }
    }
}
