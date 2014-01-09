using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace AccuV.UI.Utility
{
    public static class XmlUtility
    {
        public static string Encode(string input)
        {

            string result = XmlConvert.EncodeName(input);

            // In addition to the XmlConverted characters we also convert ".", "-"
            result = result.Replace(".", "_x002E_");
            result = result.Replace("-", "_x002D_");

            return result;
        }


        public static string Decode(string input)
        {

            string result = XmlConvert.DecodeName(input);

            // In addition to the XmlConverted characters we also decode ".", "-"
            result = result.Replace("_x002E_", ".");
            result = result.Replace("_x002D_", "-");

            return result;
        }
    }
}