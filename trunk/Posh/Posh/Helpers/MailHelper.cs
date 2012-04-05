using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Web;

namespace Posh.Helpers
{

    public class MailHelper
    {
        private readonly string _fromAddress;
        private readonly string _displayName;
        public MailHelper(string fromAddress, string displayName)
        {
            _fromAddress = fromAddress;
            _displayName = displayName;
        }

        public bool SendMessage(MailAddress to, string body, string subject, bool isBodyHtml)
        {
            var client = new SmtpClient();
            bool result = true;
            try
            {
                var message = new MailMessage { Body = body, Subject = subject };
                message.To.Add(to);
                message.From = new MailAddress(_fromAddress, _displayName);
                message.IsBodyHtml = isBodyHtml;
                client.Send(message);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool SendMessage(List<MailAddress> to, string body, string subject, bool isBodyHtml)
        {
            SmtpClient client = new SmtpClient();
            bool result = true;
            try
            {
                MailMessage message = new MailMessage {Body = body, Subject = subject};
                to.ForEach(t => message.To.Add(t));
                message.From = new MailAddress(_fromAddress, _displayName);
                message.IsBodyHtml = isBodyHtml;
                client.Send(message);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool SendTemplate(List<MailAddress> to, string template, bool isBodyHtml)
        {
            return SendTemplate(to, string.Empty, template, string.Empty, isBodyHtml, null);
        }

        public bool SendTemplate(List<MailAddress> to, string subject, string template, string language, bool isBodyHtml, params object[] replacements)
        {
            string languageFolder = (string.IsNullOrEmpty(language)) ? string.Empty : language + "/";
            string filePath = HttpContext.Current.Server.MapPath("~/Content/MailTemplates/" + languageFolder + template);
            FileStream file = new FileStream(filePath, FileMode.Open);
            StreamReader reader = new StreamReader(file);
            string body = reader.ReadToEnd();
            string formattedBody = (replacements != null && replacements.Length > 0) ? string.Format(body, replacements) : body;
            reader.Close();
            return SendMessage(to, formattedBody, subject, isBodyHtml);
        }
    }

}