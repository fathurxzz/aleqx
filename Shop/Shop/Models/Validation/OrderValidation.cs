using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    [MetadataType(typeof(OrderValidation))]
    public partial class Order
    {

    }
    
    public class OrderValidation
    {
        [DisplayName("Номер заказа")]
        public int Id { get; set; }

        [DisplayName("Дата заказа")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Контактный телефон")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Адрес доставки")]
        public string DeliveryAddress { get; set; }

        [DisplayName("Электронная почта (не обязательно)")]
        public string Email { get; set; }
    }

    
}