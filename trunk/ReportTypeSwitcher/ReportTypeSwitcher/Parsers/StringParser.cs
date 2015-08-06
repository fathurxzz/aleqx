using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Parsers
{
    class StringParser : ArgsParser<string>
    {
        public StringParser(object defaultValue = null)
            : base(defaultValue)
        {

        }

        public override string ParseArgs(IList<string> args)
        {
            var result = args[0];
            return result;
        }
    }
}
