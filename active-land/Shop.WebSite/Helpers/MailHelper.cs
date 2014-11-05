using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using Shop.DataAccess.Entities;
using Shop.WebSite.Models;

namespace Shop.WebSite.Helpers
{
    public class MailHelper
    {
        public static void Notify(Order order, int number)
        {
            string[] mailTo = ConfigurationManager.AppSettings["mailTo"].Split(new[] { " ", ";", "," }, StringSplitOptions.RemoveEmptyEntries);
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();
            foreach (var mailAddress in mailTo)
            {
                message.To.Add(mailAddress);
            }
            if (order.Subscribed && !string.IsNullOrEmpty(order.CustomerEmail))
            {
                message.To.Add(order.CustomerEmail);
            }
            message.Subject = "active-land заказ";
            message.Body =
                string.Format(
                    "<div>Номер заказа: {8}</div><div>Имя: {0}</div><div>Телефон: {1}</div><div>Email: {2}</div><div>Город: {3}</div><div>Улица: {4}</div><div>Дом: {5}</div><div>Способ оплаты: {6}</div><div>Способ доставки: {7}</div>",
                    order.CustomerName,
                    order.CustomerPhone,
                    order.CustomerEmail,
                    order.DeliveryCity,
                    order.DeliveryStreet,
                    order.DeliveryOffice,
                    order.PaymentMethod == 0 ? "Наличными" : "Картой",
                    order.DeliveryMethod == 0 ? "Доставка" : "Самовывоз",
                    number);

            foreach (var orderItem in order.OrderItems)
            {
                message.Body += string.Format(
                    "<div>Название: {0}</div><div>Артикул: {1}</div><div>Цвет: {2}</div><div>Размер: {3}</div>",
                    orderItem.ProductTitle,
                    orderItem.ProductStockNumber,
                    orderItem.ProductColor,
                    orderItem.ProductSize);
            }

            message.IsBodyHtml = true;
//#if !DEBUG
            client.Send(message);
//#endif
            message.Dispose();
        }

        public static void Notify(FeedbackForm feedbackForm)
        {
            string[] mailTo = ConfigurationManager.AppSettings["mailTo"].Split(new[] { " ", ";", "," }, StringSplitOptions.RemoveEmptyEntries);
            SmtpClient client = new SmtpClient();
            MailMessage message = new MailMessage();
            foreach (var mailAddress in mailTo)
            {
                message.To.Add(mailAddress);
            }
            message.Subject = "active-land форма обратной связи";
            message.Body =
                string.Format(
                    "<div><div>Имя: {0}</div><div>Телефон: {1}</div><div>Email: {2}</div><div>Сообщение: {3}</div>",
                    feedbackForm.Name,
                    feedbackForm.Phone,
                    feedbackForm.Email,
                    feedbackForm.Question);
            message.IsBodyHtml = true;
//#if !DEBUG
            client.Send(message);
//#endif
            message.Dispose();
        }
    }
}