using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Shop.WebSite.Helpers
{
    public static class CatalogueFilterHelper
    {
        public static string GetFilterStringForCheckbox(string[] source, string value, bool isChecked)
        {
            var result = new StringBuilder();
            if (isChecked)
            {
                foreach (var s in source.Where(s => s != value))
                {
                    if (result.Length == 0)
                    {
                        result.Append(s);
                    }
                    else
                    {
                        result.Append("-");
                        result.Append(s);
                    }
                }
            }
            else
            {
                foreach (var s in source)
                {
                    if (result.Length == 0)
                    {
                        result.Append(s);
                    }
                    else
                    {
                        result.Append("-");
                        result.Append(s);
                    }
                }

                if (result.Length == 0)
                {
                    result.Append(value);
                }
                else
                {
                    result.Append("-");
                    result.Append(value);
                }

                
            }
            return result.ToString();
        }
    }
}