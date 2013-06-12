using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ego.Models
{
    [MetadataType(typeof(OrderValidation))]
    public partial class Order
    {

    }

    public class OrderValidation
    {
        [Required(ErrorMessage = "Ваше имя - обязательное к заполнению поле!")]
        [DisplayName("Ваше имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Номер телефона - обязательное к заполнению поле!")]
        [DisplayName("Номер телефона")]
        public string Phone { get; set; }
        
        [DisplayName("Электропочта")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Неверно введен адрес почты. Формат: name@domain.com")]
        public string Email { get; set; }

        [DisplayName("Размер")]
        public string Size { get; set; }

        [DisplayName("Дополнительные комментарии")]
        public string Description { get; set; }
    }
}