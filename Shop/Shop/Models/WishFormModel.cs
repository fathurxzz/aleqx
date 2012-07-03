using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class WishFormModel : IValidatableObject
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("ФИО")]
        public string UserName { get; set; }


        [DisplayName("Категория товара")]
        public string Category { get; set; }

        [DisplayName("Бренд")]
        public string Brand { get; set; }

        [DisplayName("Наименование товара")]
        public string Title { get; set; }

        [DisplayName("Размер (точный/примерный)")]
        public string Size { get; set; }

        [DisplayName("Цвет")]
        public string Color { get; set; }

        [DisplayName("Телефон")]
        public string Phone { get; set; }

        [DisplayName("e-mail")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Неверно введен адрес почты. Формат: name@domain.com")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Обязательно!")]
        //[DisplayName("Текст")]
        //public string Text { get; set; }

        [DisplayName("Антиспам-тест: сколько будет два плюс два? (вводите цифрой)")]
        [Required(ErrorMessage = "Введите ответ!")]
        public string AntiSpamAnswer { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AntiSpamAnswer != "4")
            {
                yield return new ValidationResult("Введите правильный ответ", new string[] { "AntiSpamAnswer" });
            }
        }
    }
}