using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Parsers
{
    class StringParser : ArgsParser<string>
    {
        private int _argsCount = 1;

        private readonly ArgsParserOptions<string> _options;
        public StringParser(ArgsParserOptions<string> options = null)
        {
            _options = options;
        }

        public override int ArgsCount { get { return _argsCount; } }

        public override string ParseArgs(IList<string> args)
        {
            if (args.Count > 0)
            {
                var result = args[0];
                return result;
            }
            else
            {
                _argsCount = 0;
            }
            return _options.DefaultValue;

        }
    }
}
