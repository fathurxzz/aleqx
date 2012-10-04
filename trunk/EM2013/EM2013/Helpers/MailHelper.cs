using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using SiteExtensions;

namespace EM2013.Helpers
{
    public class MailHelper
    {
        public static MailSendingResult SendTemplate(MailAddress from, List<MailAddress> to, string subject, string template, string language, bool isBodyHtml, params object[] replacements)
        {
            string languageFolder = (string.IsNullOrEmpty(language)) ? string.Empty : language + "/";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/MailTemplates/" + languageFolder + template);
            FileStream file = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string body = reader.ReadToEnd();
            string formattedBody = (replacements != null && replacements.Length > 0) ? string.Format(body, replacements) : body;
            reader.Close();
            return SiteExtensions.MailHelper.SendMessage(from, to, formattedBody, subject, isBodyHtml);
        }
    }
}