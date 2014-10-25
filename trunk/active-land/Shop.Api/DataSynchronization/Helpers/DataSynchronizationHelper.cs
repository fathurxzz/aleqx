using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Api.DataSynchronization.Helpers
{
    public static class DataSynchronizationHelper
    {
        public static string ConvertToStringWithSeparators(List<string> source)
        {
            if (!source.Any())
            {
                return null;
            }
            var sb = new StringBuilder();
            for (int i = 0; i < source.Count(); i++)
            {
                if (i == source.Count() - 1)
                {
                    sb.Append(source[i]);
                }
                else
                {
                    sb.Append(source[i] + "#");
                }
            }
            return sb.ToString();
        }
    }
}
