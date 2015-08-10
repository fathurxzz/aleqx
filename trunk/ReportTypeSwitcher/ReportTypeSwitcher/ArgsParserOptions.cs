using System.Collections.Generic;

namespace ReportTypeSwitcher
{
    class ArgsParserOptions<T>
    {
        public T DefaultValue { get; set; }
        public IEnumerable<T> AcceptedValues { get; set; }
    }
}