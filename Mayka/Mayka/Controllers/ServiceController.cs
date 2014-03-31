using System.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace Mayka.Controllers
{
    public class ServiceController : ApiController
    {
        [AllowAnonymous]
        public void NotifyMiller(string phone, string filename)
        {
            string previewUrl = ConfigurationManager.AppSettings["previewUrl"];
            if (string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(previewUrl))
                throw new HttpException(400, "Phone and url are required");
            string url = previewUrl + filename;
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();
            //message.To.Add("kushko.alex@gmail.com");
            message.To.Add("miller.kak.miller@gmail.com");
            message.Subject = "m-j - Перезвони по футболке";
            message.Body = string.Format("<div>Телефон: {0}</div><div><img src=\"{1}\" /></div>", phone, url);
            message.IsBodyHtml = true;
            client.Send(message);
            message.Dispose();
        }

    }
}
