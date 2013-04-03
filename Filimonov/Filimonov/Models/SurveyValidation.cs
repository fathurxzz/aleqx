using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Filimonov.Models
{
    [MetadataType(typeof(SurveyValidation))]
    public partial class Survey
    {
        public int CustomerId { get; set; }
    }

    public class SurveyValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        //[DisplayName("Дата")]
        //public DateTime Date { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Клиент")]
        public string CustomerId { get; set; }
    }
}