using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Parsers
{
    class ArgsParserSequence
    {
        private readonly IDictionary<ReportArguments, IArgsParser> _parsers;

        public ArgsParserSequence()
        {
            _parsers = new Dictionary<ReportArguments, IArgsParser>();
        }

        public ArgsParserSequence(IDictionary<ReportArguments, IArgsParser> parsers)
        {
            _parsers = parsers;
        }

        public void AddParser(ReportArguments name, IArgsParser parser)
        {
            _parsers.Add(name, parser);
        }

        public IDictionary<ReportArguments, object> ParseArgs(string[] args)
        {
            var result = new Dictionary<ReportArguments, object>();
            var argsToParse = args.ToList();
            foreach (var parser in _parsers)
            {
                result.Add(parser.Key, parser.Value.ParseArgs(argsToParse));
                if (parser.Value is GuidParser)
                {
                    if (argsToParse.Count != 0)
                    {
                        argsToParse.RemoveRange(argsToParse.Count - 1, parser.Value.ArgsCount);
                    }
                }
                else
                {
                    argsToParse.RemoveRange(0, parser.Value.ArgsCount);
                }
                
            }
            return result;
        }
    }
}
