using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher.Parsers
{
    class GuidParser:ArgsParser<Guid?>
    {
        private int _argsCount = 1;
        public override int ArgsCount { get { return _argsCount; } }
        public override Guid? ParseArgs(IList<string> args)
        {
            if (args.Count == 0)
            {
                _argsCount = 0;
            }
            else
            {
                Guid token;
                if (Guid.TryParse(args.Last(), out token))
                {
                    return token;
                }    
            }
            return null;
        }
    }
}
