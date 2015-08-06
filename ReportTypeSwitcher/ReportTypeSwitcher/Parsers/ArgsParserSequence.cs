using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Parsers
{
    class ArgsParserSequence
    {
        private readonly IDictionary<string, IArgsParser> _parsers;

        public ArgsParserSequence()
        {
            _parsers = new Dictionary<string, IArgsParser>();
        }

        public ArgsParserSequence(IDictionary<string, IArgsParser> parsers)
        {
            _parsers = parsers;
        }

        public void AddParser(string name, IArgsParser parser)
        {
            _parsers.Add(name, parser);
        }

        public IDictionary<string, object> ParseArgs(string[] args)
        {
            var result = new Dictionary<string, object>();
            var argsToParse = args.ToList();
            foreach (var parser in _parsers)
            {
                result.Add(parser.Key, parser.Value.ParseArgs(argsToParse));
                argsToParse.RemoveRange(0, parser.Value.ArgsCount);
            }
            return result;
        }
    }
}
