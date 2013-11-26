using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Penetron.Models
{
    [MetadataType(typeof(UserArticleValidation))]
    public partial class UserArticle
    {

    }

    public class UserArticleValidation
    {

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Идентификатор")]
        public string Name { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }


    }
}