using System.Collections.Generic;

namespace ReportTypeSwitcher
{
    abstract class ArgsParser<TArgs> : IArgsParser<TArgs>
    {
        public virtual int ArgsCount { get { return 1; } }
        object IArgsParser.ParseArgs(IList<string> args)
        {
            return ParseArgs(args);
        }

        public TArgs DefaultValue { get; set; }

        public abstract TArgs ParseArgs(IList<string> args);
    }
}