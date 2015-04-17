using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ConsoleApplication1.Helpers
{
    class HtmlHelper
    {
        private static string _baseUrl = "http://auto.ria.com";
        private static Regex _trimmer = new Regex(@"\s\s+");

        public static HtmlDocument GetHtmlDocument(string url, ref double size)
        {
            GetHtmlResult htmlResult = HttpHelper.GetHtml(url);
            if (!htmlResult.Success)
            {
                throw new Exception(htmlResult.ErrorMessage);
            }
            var decodedHtml = System.Web.HttpUtility.HtmlDecode(htmlResult.ResultHtml);
            var doc = new HtmlDocument();
            doc.LoadHtml(decodedHtml);
            size += htmlResult.DownloadedDataSize;
            return doc;
        }

        private static Dictionary<string, string> GetRiaEntity(HtmlDocument htmlDocument, bool isCar = false, bool onlyPagingLinks = false)
        {
            var result = new Dictionary<string, string>();
            HtmlNode node = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='content']");
            HtmlNodeCollection nodes = node.SelectNodes(".//a");
            foreach (var n in nodes)
            {
                if (n.HasAttributes)
                {
                    if (n.Attributes[0] != null && ((isCar && !n.Attributes[0].Value.Contains("/map/bu")) || !n.Attributes[0].Value.Contains(".html")))
                    {
                        result.Add(_baseUrl + n.Attributes[0].Value, n.InnerText);
                    }
                }
            }
            return result;
        }

        public static List<string> GetPages(HtmlDocument htmlDocument)
        {
            var result = new List<string>();
            HtmlNode node = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='content']");
            HtmlNodeCollection nodes = node.SelectNodes(".//a");
            foreach (var n in nodes)
            {
                if (n.HasAttributes)
                {
                    if (n.Attributes[0] != null && (n.Attributes[0].Value.Contains(".html")) &&n.Attributes[0].Value.Contains("/map/bu"))
                    {
                        result.Add(_baseUrl + n.Attributes[0].Value);
                    }
                }
            }
            return result;
        }

        public static Dictionary<string, string> GetBrands(HtmlDocument htmlDocument)
        {
            return GetRiaEntity(htmlDocument);
        }
        
        public static Dictionary<string, string> GetModels(HtmlDocument htmlDocument)
        {
            return GetRiaEntity(htmlDocument);
        }

        public static Dictionary<string, string> GetCars(HtmlDocument htmlDocument)
        {
            return GetRiaEntity(htmlDocument, true);
        }

        public static void ReadCarAttributes(ref Car realCar, IEnumerable<HtmlNode> nodes, bool swapKeyValues = false)
        {
            foreach (var n in nodes)
            {
                var s = n.SelectSingleNode(".//strong");
                var keyValue = _trimmer.Replace(n.InnerText.Replace("\n", ""), " ").Replace("Указать", "").Trim();
                if (s != null)
                {
                    var value = _trimmer.Replace(s.InnerText.Replace("\n", ""), " ").Trim();
                    var key = !string.IsNullOrEmpty(value) ? keyValue.Replace(value, "") : keyValue;
                    key = key.Trim();
                    if (string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                    {
                        realCar.attributes.Add(value, value);
                    }
                    else if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                    {
                        if (swapKeyValues)
                        {
                            realCar.attributes.Add(value, key);
                        }
                        else
                        {
                            realCar.attributes.Add(key, value);
                        }
                    }
                }
                else
                {
                    realCar.attributes.Add(keyValue, keyValue);
                }
            }
        }
    }
}
