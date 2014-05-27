using System.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace Edoki.Controllers
{
    public class ServiceController : ApiController
    {
        [HttpPost]
        public void MakeOrder(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                throw new HttpException(400, "Phone is required");
            var client = new SmtpClient();
            var message = new MailMessage();
            message.To.Add("kushko.alex@gmail.com");
            //message.To.Add("miller.kak.miller@gmail.com");
            message.Subject = "Edoki - Заказ";
            message.Body = string.Format("<div>Телефон: {0}</div>", phone);
            message.IsBodyHtml = true;
            client.Send(message);
            message.Dispose();
        }
    }
}
