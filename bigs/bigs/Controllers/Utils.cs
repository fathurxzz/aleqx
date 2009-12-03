using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bigs.Models;

namespace bigs.Controllers
{
    public static class Utils
    {
        public static SiteContent GetContent(string contentUrl)
        {
            using (DataStorage context = new DataStorage())
            {
                SiteContent result = context.SiteContent.Where(c => c.Url == contentUrl).Select(c => c).FirstOrDefault();
                if (result == null)
                    return result;
                context.Detach(result);
                return result;
            }
        }

        public static void SetText(string contentUrl, string text, string title, string keywords, string description)
        {
            using (DataStorage context = new DataStorage())
            {
                SiteContent content = context.SiteContent.Where(sc => sc.Url == contentUrl).Select(sc => sc).First();
                content.Text = text;
                content.Title = title;
                content.Keywords = keywords;
                content.Description = description;
                context.SaveChanges();
            }
        }
    }
}
