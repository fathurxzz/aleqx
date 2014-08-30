using System.Configuration;
using System.Net.Mail;

namespace Kiki.WebSite.Helpers
{
    public class MailHelper
    {
        //public static void Notify(Order order,int number)
        //{
        //    string mailTo = ConfigurationManager.AppSettings["mailTo"];
        //    SmtpClient client = new SmtpClient();
        //    MailMessage message = new MailMessage();
        //    message.To.Add(mailTo);
        //    if (order.Subscribed&&!string.IsNullOrEmpty(order.CustomerEmail))
        //    {
        //        message.To.Add(order.CustomerEmail);
        //    }
        //    message.Subject = "active-land заказ";
        //    message.Body =
        //        string.Format(
        //            "<div>Номер заказа: {8}</div><div>Имя: {0}</div><div>Телефон: {1}</div><div>Email: {2}</div><div>Город: {3}</div><div>Улица: {4}</div><div>Дом: {5}</div><div>Способ оплаты: {6}</div><div>Способ доставки: {7}</div>",
        //            order.CustomerName,
        //            order.CustomerPhone,
        //            order.CustomerEmail,
        //            order.DeliveryCity,
        //            order.DeliveryStreet,
        //            order.DeliveryOffice,
        //            order.PaymentMethod == 0 ? "Наличными" : "Картой",
        //            order.DeliveryMethod == 0 ? "Доставка" : "Самовывоз",
        //            number);
        //    message.IsBodyHtml = true;
        //    client.Send(message);
        //    message.Dispose();
        //}
    }
}