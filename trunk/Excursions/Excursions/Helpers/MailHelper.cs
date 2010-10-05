using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Excursions.Helpers
{
    public class MailHelper
    {
        public static bool SendMessage(string from, List<MailAddress> to, string body, string subject, bool isBodyHtml)
        {
            var client = new SmtpClient();
            bool result = true;
            try
            {
                var message = new MailMessage {Body = body, Subject = subject};
                to.ForEach(t => message.To.Add(t));
                message.From = new MailAddress(from);
                message.IsBodyHtml = isBodyHtml;
                client.Send(message);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool SendTemplate(string from, List<MailAddress> to, string template, bool isBodyHtml)
        {
            return SendTemplate(from, to, string.Empty, template, isBodyHtml, null);
        }

        public static bool SendTemplate(string from, string to, string subject, string template, bool isBodyHtml, params object[] replacements)
        {
            List < MailAddress > mailAddresses;
            try
            {
                string[] separator = new string[] {";", " ", ","};
                string[] x = to.Split( separator , StringSplitOptions.None);
                mailAddresses = (from s in x where !string.IsNullOrEmpty(s.Trim()) select new MailAddress(s.Trim())).ToList();
            }
            catch
            {
                return false;
            }
            return SendTemplate(from, mailAddresses, subject, template, isBodyHtml, replacements);
        }

        public static bool SendTemplate(string from, List<MailAddress> to, string subject, string template, bool isBodyHtml, params object[] replacements)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/Content/MailTemplates/" + template);
            var file = new FileStream(filePath, FileMode.Open);
            var reader = new StreamReader(file);
            string body = reader.ReadToEnd();
            string formattedBody = (replacements != null && replacements.Length > 0) ? string.Format(body, replacements) : body;
            reader.Close();
            return SendMessage(from, to, formattedBody, subject, isBodyHtml);
        }
    }
}