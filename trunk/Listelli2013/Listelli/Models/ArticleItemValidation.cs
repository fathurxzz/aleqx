using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Listelli.Models
{
    [MetadataType(typeof(ArticleItemValidation))]
    public partial class ArticleItem
    {

    }

    public class ArticleItemValidation
    {
        [DisplayName("Текст")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Порядок отображения")]
        public int SortOrder { get; set; }
    }
}