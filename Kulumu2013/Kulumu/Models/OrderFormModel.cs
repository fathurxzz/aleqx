using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kulumu.Models
{
    public class OrderFormModel : IValidatableObject
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public IEnumerable<ProductSize> ProductSizes { get; set; }
            
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Ваше имя:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Электронная почта:")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Неверно введен адрес почты. Формат: name@domain.com")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Телефон:")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Адрес:")]
        public string Address { get; set; }

        /*[Required(ErrorMessage = "Обязательно!")]*/
        [DisplayName("Веберите размер:")]
        public string Size { get; set; }

        [DisplayName("Сколько будет 2+2 (цифрой):")]
        [Required(ErrorMessage = "Введите ответ!")]
        public string AntiSpamAnswer { get; set; }

        public string ErrorMessage { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AntiSpamAnswer != "4")
            {
                yield return new ValidationResult("Введите правильный ответ", new string[] { "AntiSpamAnswer" });
            }
        }
    }
}