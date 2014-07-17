using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace SiteExtensions
{
    public class MailSendingResult
    {
        public string ErrorMessage { get; set; }
        public bool EmailSent { get; set; }
    }

    public class MailHelper
    {
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
    }
}
