using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportTypeSwitcher
{
    public interface IArgsParser
    {
        int ArgsCount { get; }
        object ParseArgs(IList<string> args);
    }

    public interface IArgsParser<out TArgs>:IArgsParser
    {
        TArgs ParseArgs(IList<string> args);
    }

    abstract class ArgsParser<TArgs>:IArgsParser<TArgs>
    {
        protected ArgsParser(object defaultValue = null)
        {
            
        }

        public virtual int ArgsCount { get { return 1; } }
        object IArgsParser.ParseArgs(IList<string> args)
        {
            return ParseArgs(args);
        }

        public abstract TArgs ParseArgs(IList<string> args);
    }

    interface IReport
    {
        string ReportType { get; }
        void Execute(string[] args);
    }
}
