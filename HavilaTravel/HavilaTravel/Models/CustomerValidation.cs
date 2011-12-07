using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HavilaTravel.Models
{
    [MetadataType(typeof(CustomerValidation))]
    public partial class Customers
    {

    }

    public class CustomerValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Электронная почта")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Неверно введен адрес почты. Формат: name@domain.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        public int SubscribeType{ get; set; }
    }
}