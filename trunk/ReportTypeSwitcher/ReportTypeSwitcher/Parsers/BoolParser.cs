using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Parsers
{
    class BoolParser:ArgsParser<bool>
    {
        public override bool ParseArgs(IList<string> args)
        {
            if(!(new[] {"0", "1", "true", "false"}).Contains(args[0].ToLower()))
                throw new ArgumentException("Cannot parse " + args[0] + " to Boolean");
            return args[0] == "1" || args[0].ToLower() == "true";
        }
    }
}
