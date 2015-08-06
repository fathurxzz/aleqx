using System.Collections.Generic;

namespace ReportTypeSwitcher
{
    public interface IArgsParser
    {
        int ArgsCount { get; }
        object ParseArgs(IList<string> args);
    }

    public interface IArgsParser<out TArgs> : IArgsParser
    {
        TArgs ParseArgs(IList<string> args);
    }
}