using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Filimonov.Models
{
    [MetadataType(typeof(LayoutValidation))]
    public partial class Layout
    {

    }

    public class LayoutValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Название")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Идентификатор")]
        public string Name { get; set; }

    }
}