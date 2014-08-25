using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    [MetadataType(typeof(OrderValidation))]
    partial class Order
    {

    }

    class OrderValidation
    {
        [Display(Name = "Дата")]
        public string Date { get; set; }

        [Display(Name = "Имя")]
        public string CustomerName { get; set; }

        [Display(Name = "Телефон")]
        public string CustomerPhone { get; set; }

        [Display(Name = "Email")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Обработан")]
        public bool Completed { get; set; }

        [Display(Name = "Дополнительная информация")]
        public string Info { get; set; }

        [Display(Name = "Адрес доставки")]
        public string DeliveryAddress { get; set; }

        [Display(Name = "Подписан на рассылку")]
        public bool Subscribed { get; set; }

        [Display(Name = "Способ доставки")]
        public int DeliveryMethod { get; set; }

        [Display(Name = "Город")]
        public bool DeliveryCity { get; set; }

        [Display(Name = "Улица")]
        public bool DeliveryStreet { get; set; }

        [Display(Name = "Квартира / Офис")]
        public bool DeliveryOffice { get; set; }

        [Display(Name = "Способ оплаты")]
        public int PaymentMethod { get; set; }
    }

}
