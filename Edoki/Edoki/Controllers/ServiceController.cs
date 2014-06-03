using System.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;

namespace Edoki.Controllers
{
    class Order
    {
        public string phone { get; set; }
        public string from { get; set; }
    }

    public class ServiceController : ApiController
    {
        //[HttpPost]
        //public void MakeOrder([FromBody] string phone, [FromBody]string from)
        //{
        //    if (string.IsNullOrEmpty(phone))
        //        throw new HttpException(400, "Phone is required");
        //    var client = new SmtpClient();
        //    var message = new MailMessage();
        //    message.To.Add("kushko.alex@gmail.com");
        //    //message.To.Add("miller.kak.miller@gmail.com");
        //    //message.To.Add("irenepinchuk@gmail.com");
        //    message.Subject = string.Format("Edoki - Заказ - {0}", from);
        //    message.Body = string.Format("<div>Телефон: {0}</div><div>Страница: {1}</div>", phone, from);
        //    message.IsBodyHtml = true;
        //    client.Send(message);
        //    message.Dispose();
        //}


        [HttpPost]
        public void MakeOrder([FromBody]object order)
        {
            JObject obj = JObject.FromObject(order);
            var phone = obj["phone"].ToString();
            var fromPage = obj["from"].ToString();
            
            if (string.IsNullOrEmpty(phone))
                throw new HttpException(400, "Phone is required");
            var client = new SmtpClient();
            var message = new MailMessage();
            message.To.Add("kushko.alex@gmail.com");
            message.To.Add("miller.kak.miller@gmail.com");
            message.To.Add("irenepinchuk@gmail.com");
            message.Subject = string.Format("Edoki - Заказ - {0}", fromPage);
            message.Body = string.Format("<div>Телефон: {0}</div><div>Страница: {1}</div>", phone, fromPage);
            message.IsBodyHtml = true;
            client.Send(message);
            message.Dispose();
            
        }
    }
}
