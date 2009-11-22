using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using bigs.Models;

namespace bigs.Controllers
{
    public static class Utils
    {
        public static SiteContent GetText(string contentUrl)
        {
            using (DataStorage context = new DataStorage())
            {
                SiteContent result = context.SiteContent.Where(c => c.Url == contentUrl).Select(c => c).First();
                context.Detach(result);
                //if (SystemSettings.CurrentLanguage != result.Language)
                //    SystemSettings.CurrentLanguage = result.Language;
                return result;
            }
        }

        public static void SetText(string contentUrl, string value)
        {
            using (DataStorage context = new DataStorage())
            {
                SiteContent text = context.SiteContent.Where(sc => sc.Url == contentUrl).Select(sc => sc).First();
                text.Text = value;
                context.SaveChanges();
            }
        }

        public static void UpdateButtonStatuses()
        {
 
        }

        
    }
}
