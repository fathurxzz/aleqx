using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Parsers
{
    class IntParser : ArgsParser<int>
    {
        private int _argsCount = 1;

        private readonly ArgsParserOptions<int> _options;
        public IntParser(ArgsParserOptions<int> options = null)
        {
            _options = options;
        }

        public override int ArgsCount { get { return _argsCount; } }

        public override int ParseArgs(IList<string> args)
        {
            if (args.Count > 0)
            {
                return int.Parse(args[0]);
            }
            
            if (_options != null)
            {
                _argsCount = 0;
                return _options.DefaultValue;
            }

            throw new Exception("Default value for IntParser is not set");
        }
    }
}
