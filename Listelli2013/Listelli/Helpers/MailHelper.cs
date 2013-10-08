using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using Listelli.Areas.Admin.Controllers;
using Listelli.Models;
using SiteExtensions;

namespace Listelli.Helpers
{
    public class MailHelper
    {

        private static int _portionSize = 10;

        private static IEnumerable<List<MailAddress>> SplitMailAdresses(List<MailAddress> source)
        {
            int numberOfPortions;
            if (source.Count % _portionSize > 0)
                numberOfPortions = (source.Count / _portionSize) + 1;
            else
                numberOfPortions = (source.Count / _portionSize);

            List<MailAddress>[] portions = new List<MailAddress>[numberOfPortions];

            for (int i = 0; i < source.Count; i++)
            {
                int currentPortion = i / _portionSize;
                var currentItem = source[i];

                if (portions[currentPortion] == null)
                    portions[currentPortion] = new List<MailAddress>();

                portions[currentPortion].Add(currentItem);
            }

            return portions;
        }


        public static MailSendingResult SendTemplateByPortions(MailAddress from, List<MailAddress> to, string subject, string template, string language, bool isBodyHtml, params object[] replacements)
        {
            string languageFolder = (string.IsNullOrEmpty(language)) ? string.Empty : language + "/";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/MailTemplates/" + languageFolder + template);
            FileStream file = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string body = reader.ReadToEnd();
            string formattedBody = (replacements != null && replacements.Length > 0) ? string.Format(body, replacements) : body;
            reader.Close();

            var result = new MailSendingResult();

            try
            {
                var portions = SplitMailAdresses(to);
                foreach (List<MailAddress> mailAddresses in portions)
                {
                    SiteExtensions.MailHelper.SendMessage(from, mailAddresses, formattedBody, subject, isBodyHtml);
                }
            }
            catch
            {
                result.EmailSent = false;
            }
            return result;
        }


        public static MailSendingResult SendTemplate(MailAddress from, List<MailAddress> to, string subject, string template, string language, bool isBodyHtml, params object[] replacements)
        {
            string languageFolder = (string.IsNullOrEmpty(language)) ? string.Empty : language + "/";


            //string filePath = HttpContext.Current.Server.MapPath("~/Content/MailTemplates/" + languageFolder + template);

            string filePath = Path.Combine(HttpRuntime.AppDomainAppPath, "Content","MailTemplates" , languageFolder , template);

            FileStream file = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string body = reader.ReadToEnd();
            string formattedBody = (replacements != null && replacements.Length > 0) ? string.Format(body, replacements) : body;
            reader.Close();
            return SiteExtensions.MailHelper.SendMessage(from, to, formattedBody, subject, isBodyHtml);
        }


        public static SendEmailStatus CreateSendEmailStatusInstance(int articleId, int subscriberId)
        {
            return new SendEmailStatus
                   {
                       Date = DateTime.Now,
                       SendDate = DateTime.Now,
                       Status = 0,
                       ArticleId = articleId,
                       SubscriberId = subscriberId,
                       Attempt = 0
                   };
        }
    }
}