using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Leo.Models
{
    public class FeedbackFormModel
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Имя")]
        public string Name { get; set; }
        [DisplayName("Электронная почта")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Неверно введен адрес почты. Формат: name@domain.com")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Текст")]
        public string Text { get; set; }

        [DisplayName("Антиспам-тест: сколько будет два плюс два? (вводите цифрой)")]
        [Required(ErrorMessage = "Введите ответ!")]
        [RegularExpression(@"4", ErrorMessage = "Введите правильный ответ")]
        public string AntiSpamAnswer { get; set; }
    }
}