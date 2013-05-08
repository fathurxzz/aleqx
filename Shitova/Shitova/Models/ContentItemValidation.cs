using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shitova.Models
{
    [MetadataType(typeof(ContentItemValidation))]
    public partial class ContentItem
    {

    }

    public class ContentItemValidation
    {
        [DisplayName("Текст")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }
    }
}