using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Parsers
{
    class PortfoliosParser : ArgsParser<List<string>>
    {
        public override List<string> ParseArgs(IList<string> args)
        {
            if (args.Count > 0)
            {
                return args[0].Split(new[] {";"}, StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            throw new Exception("Default value for PortfoliosParser is not set");
        }
    }
}
