using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EM2013.Helpers
{
    public class MailHelper
    {
        public class MailSendingResult
        {
            public string ErrorMessage { get; set; }
            public bool EmailSent { get; set; }
        }

        public static MailSendingResult SendMessage(MailAddress from, List<MailAddress> to, string body, string subject, bool isBodyHtml)
        {
            SmtpClient client = new SmtpClient();
            try
            {
                MailMessage message = new MailMessage
                {
                    Body = body,
                    Subject = subject,
                    IsBodyHtml = isBodyHtml
                };
                to.ForEach(t => message.To.Add(t));
                if (from != null)
                    message.From = from;
                client.Send(message);

                return new MailSendingResult { EmailSent = true };
            }
            catch (Exception ex)
            {
                return new MailSendingResult { EmailSent = false, ErrorMessage = ex.Message + " " + ex.InnerException };
            }
        }

        public static MailSendingResult SendTemplate(MailAddress from, List<MailAddress> to, string subject, string template, string Language, bool isBodyHtml, params object[] replacements)
        {
            string languageFolder = (string.IsNullOrEmpty(Language)) ? string.Empty : Language + "/";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/MailTemplates/" + languageFolder + template);
            FileStream file = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string body = reader.ReadToEnd();
            string formattedBody = (replacements != null && replacements.Length > 0) ? string.Format(body, replacements) : body;
            reader.Close();
            return SendMessage(from, to, formattedBody, subject, isBodyHtml);
        }
    }
}