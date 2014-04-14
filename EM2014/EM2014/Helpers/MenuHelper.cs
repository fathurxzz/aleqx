using System;
using System.Linq;
using System.Web;

namespace EM2014.Helpers
{
    public static class MenuHelper
    {
        public static string ToSpecificmenuTitle(this string source)
        {
            string result = "";
            var x = source.Split(new[] {" "}, StringSplitOptions.None);
            int i = 0;
            foreach (string s in x)
            {
                i++;

                if (i != x.Count())
                    result += s + "&nbsp; &nbsp;";
                else
                    result += s;

            }
            return HttpUtility.HtmlDecode(result);
        }
    }
}