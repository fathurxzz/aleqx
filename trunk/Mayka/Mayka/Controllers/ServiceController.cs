using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace Mayka.Controllers
{
    public class ServiceController : ApiController
    {
        [AllowAnonymous]
        public void NotifyMiller(string phone, string url)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(phone))
                throw new HttpException(400, "Phone and url are required");
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();
            message.To.Add("kushko.alex@gmail.com");
            message.Subject = "m-j - Перезвони по футболке";
            message.Body = string.Format("<div>Телефон: {0}</div><div><a href=\"{1}\">тыц!</a></div>", phone, url);
            message.IsBodyHtml = true;
            client.Send(message);
            message.Dispose();
        }

    }
}
