using System.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace Edoki.Controllers
{
    public class ServiceController : ApiController
    {
        [HttpPost]
        public void MakeOrder(string phone, string from)
        {
            if (string.IsNullOrEmpty(phone))
                throw new HttpException(400, "Phone is required");
            var client = new SmtpClient();
            var message = new MailMessage();
            message.To.Add("kushko.alex@gmail.com");
            message.To.Add("miller.kak.miller@gmail.com");
            message.To.Add("irenepinchuk@gmail.com");
            message.Subject = "Edoki - Заказ";
            message.Body = string.Format("<div>Телефон: {0}</div><div>Страница: {1}</div>", phone, from);
            message.IsBodyHtml = true;
            client.Send(message);
            message.Dispose();
        }
    }
}
