using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace SiteExtensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool EmailSent { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="isBodyHtml"></param>
        /// <returns></returns>
        public static ResponseData SendMessage(MailAddress from, List<MailAddress> to, string body, string subject, bool isBodyHtml)
        {
            SmtpClient client = new SmtpClient { UseDefaultCredentials = true };
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

                return new ResponseData { EmailSent = true };
            }
            catch (Exception ex)
            {
                return new ResponseData { EmailSent = false, ErrorMessage = ex.Message + " " + ex.InnerException };
            }
        }

        //public static ResponseData SendTemplate(string from, List<MailAddress> to, string template, bool isBodyHtml)
        //{
        //    return SendTemplate(from, to, string.Empty, template, string.Empty, isBodyHtml, null);
        //}

        //public static ResponseData SendTemplate(string from, List<MailAddress> to,  params object[] replacements)
        //{
        //    return SendTemplate(from, to, string.Empty, null, string.Empty, true, replacements);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="template"></param>
        /// <param name="Language"></param>
        /// <param name="isBodyHtml"></param>
        /// <param name="replacements"></param>
        /// <returns></returns>
        public static ResponseData SendTemplate(MailAddress from, List<MailAddress> to, string subject, string template, string Language, bool isBodyHtml, params object[] replacements)
        {
            //string languageFolder = (string.IsNullOrEmpty(Language)) ? string.Empty : Language + "/";
            //string filePath = HttpContext.Current.Server.MapPath("~/Content/MailTemplates/" + languageFolder + template);
            //FileStream file = new FileStream(filePath, FileMode.Open);
            //StreamReader reader = new StreamReader(file);
            //string body = reader.ReadToEnd();
            //string formattedBody = (replacements != null && replacements.Length > 0) ? string.Format(body, replacements) : body;
            //reader.Close();

            string formattedBody = "<html><head><title>Обратная связь</title></head><body><table><tr><td>Имя:</td><td>{0}</td></tr><tr><td>Электронная почта:</td><td>{1}</td></tr></table>Текст сообщения:<p>{2}</p></body></html>";
            formattedBody = (replacements != null && replacements.Length > 0) ? string.Format(formattedBody, replacements) : formattedBody;
            return SendMessage(from, to, formattedBody, subject, isBodyHtml);
        }
    }
}
