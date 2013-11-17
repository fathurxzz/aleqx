using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Penetron.Models
{
    [MetadataType(typeof(ReasonValidation))]
    public partial class Reason
    {

    }

    public class ReasonValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Текст")]
        [Required(ErrorMessage = "Обязательно!")]
        public string Text { get; set; }
    }
}