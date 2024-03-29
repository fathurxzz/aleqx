﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Posh.Helpers
{
    public class MailHelper2
    {
        public static bool SendMessage(List<MailAddress> to, string body, string subject, bool isBodyHtml)
        {
            SmtpClient client = new SmtpClient();
            bool result = true;
            try
            {
                MailMessage message = new MailMessage();
                message.Body = body;
                message.Subject = subject;
                to.ForEach(t => message.To.Add(t));
                message.From = new MailAddress("office@posh.net.ua");
                message.IsBodyHtml = isBodyHtml;
                client.Send(message);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool SendTemplate(List<MailAddress> to, string template, bool isBodyHtml)
        {
            return SendTemplate(to, string.Empty, template, string.Empty, isBodyHtml, null);
        }

        public static bool SendTemplate(List<MailAddress> to, string subject, string template, string Language, bool isBodyHtml, params object[] replacements)
        {
            string languageFolder = (string.IsNullOrEmpty(Language)) ? string.Empty : Language + "/";
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