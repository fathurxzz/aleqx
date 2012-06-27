using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Rvk.Models;

namespace Rvk.Helpers
{
    public class ResponseData
    {
        public string ErrorMessage { get; set; }
        public bool EmailSent { get; set; }
    }

    public class MailHelper
    {
        public static ResponseData SendMessage(List<MailAddress> to, string body, string subject, bool isBodyHtml)
        {
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = false;
            try
            {
                MailMessage message = new MailMessage();
                message.Body = body;
                message.Subject = subject;
                to.ForEach(t => message.To.Add(t));
                message.From = new MailAddress("info@rvk-fit.com.ua");
                message.IsBodyHtml = isBodyHtml;
                client.Send(message);

                return new ResponseData { EmailSent = true };
            }
            catch (Exception ex)
            {
                return new ResponseData { EmailSent = false, ErrorMessage = ex.Message + " " + ex.InnerException };
            }
        }

        public static ResponseData SendTemplate(List<MailAddress> to, string template, bool isBodyHtml)
        {
            return SendTemplate(to, string.Empty, template, string.Empty, isBodyHtml, null);
        }

        public static ResponseData SendTemplate(List<MailAddress> to, string subject, string template, string Language, bool isBodyHtml, params object[] replacements)
        {
            //string languageFolder = (string.IsNullOrEmpty(Language)) ? string.Empty : Language + "/";
            //string filePath = HttpContext.Current.Server.MapPath("~/Content/MailTemplates/" + languageFolder + template);
            //FileStream file = new FileStream(filePath, FileMode.Open);
            //StreamReader reader = new StreamReader(file);
            //string body = reader.ReadToEnd();
            //string formattedBody = (replacements != null && replacements.Length > 0) ? string.Format(body, replacements) : body;
            //reader.Close();

            string formattedBody =
                @"<html>
<head>
    <title>Обратная связь</title>
</head>
<body>
    <table>
        <tr>
            <td>
                Имя:            
            </td>
            <td>
                {0}
            </td>
        </tr>
        <tr>
            <td>
                Электронная почта:            
            </td>
            <td>
                {1}
            </td>
        </tr>
    </table>
    Текст сообщения:
    <p>
        {2}
    </p>
</body>
</html>";
            return SendMessage(to, formattedBody, subject, isBodyHtml);
        }
    }
}