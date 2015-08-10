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
                if ( _options!=null && _options.AcceptedValues.Any())
                {
                    if (!_options.AcceptedValues.Contains(args[0]))
                    {
                        throw new Exception("value must be in [" + _options.AcceptedValues.Aggregate((a, b) => a + ", " + b)+"]");
                    }
                }

                return args[0];
            }

            if (_options != null && _options.DefaultValue != null)
            {
                _argsCount = 0;
                return _options.DefaultValue;
            }

            throw new Exception("Default value for StringParser is not set");
        }
    }
}
