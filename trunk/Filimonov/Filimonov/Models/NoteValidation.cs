using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Filimonov.Models
{
    [MetadataType(typeof(NoteValidation))]
    public partial class Note
    {
         
    }

    public class NoteValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }
    }
}