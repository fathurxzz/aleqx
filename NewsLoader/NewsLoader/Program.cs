using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace NewsLoader
{
    class Program
    {
        public static List<string> SourseLinks = new List<string>();
        public static string RootLink = "http://fakty.ua/";

        static void Main(string[] args)
        {
            var date = DateTime.Now;

            var link = "http://fakty.ua/archive/index?d=" + date.Year + (date.Month.ToString().Length == 1 ? "0" + date.Month : date.Month.ToString()) + (date.Day.ToString().Length == 1 ? "0" + date.Day : date.Day.ToString());

            /*
            for (var i = 1; i < 2; i++)
            {
                GetSourceLinks(link + "&ArticlesItem_page="+i);
            }
            */

            GetSourceLinks(link);

            foreach (var sourseLink in SourseLinks)
            {
                GetContentAndSaveToDatabase(sourseLink);
            }
        
        }

        static string GetResponseString(string link)
        {
            var wreq = (HttpWebRequest)WebRequest.Create(link);
            wreq.Method = "GET";
            var wr = wreq.GetResponse();
            var enc = Encoding.GetEncoding(1251);
            var reader = new StreamReader(wr.GetResponseStream(), enc);
            return reader.ReadToEnd();
        }
        
        static string GetResponseString(string link,Encoding encoding)
        {
            var wreq = (HttpWebRequest)WebRequest.Create(link);
            wreq.Method = "GET";
            var wr = wreq.GetResponse();
            var reader = new StreamReader(wr.GetResponseStream(), encoding);
            return reader.ReadToEnd();
        }

        static void GetSourceLinks(string link)
        {
            var responseString = GetResponseString(link);
            var document = new HtmlDocument();
            document.LoadHtml(responseString);
            var nodes = document.DocumentNode.SelectNodes("//a[@class='tit']");
            if (nodes == null) return;
            foreach (var sLink in nodes.Cast<HtmlNode>().Select(node => node.Attributes["href"].Value).Where(sLink => !SourseLinks.Contains(sLink)))
            {
                SourseLinks.Add(sLink);
            }
        }

        static void GetContentAndSaveToDatabase(string link)
        {
            string mainImage = string.Empty;
            string content = string.Empty;
            string title = string.Empty;
            link = RootLink + link;

            var responseString = GetResponseString(link, Encoding.GetEncoding("utf-8"));
            var document = new HtmlDocument();
            document.LoadHtml(responseString);
            
            var node = document.DocumentNode.SelectSingleNode("//img[@class='fotoramka_l']");
            if (node!=null)
            {
                mainImage = node.Attributes["src"].Value;
            }

            node = document.DocumentNode.SelectSingleNode("//div[@class='tit_main_news']");
            if(node!=null)
            {
                title = node.InnerHtml;
            }

            node = document.DocumentNode.SelectSingleNode("//div[@id='article_content']");
            if (node != null)
            {
                content = node.InnerHtml;
            }


            using (var context = new ContentStorage())
            {
                var newsEntity = new News
                                     {
                                         Date = DateTime.Now,
                                         Content = content,
                                         SourceId = 1,
                                         SourceName = "Факти",
                                         Title = title,
                                         ImageSource = mainImage
                                     };
                context.AddToNews(newsEntity);
                context.SaveChanges();
            }

        }


    }
}
