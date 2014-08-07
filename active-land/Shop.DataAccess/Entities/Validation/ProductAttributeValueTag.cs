using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    [MetadataType(typeof(ProductAttributeValueTagValidation))]
    partial class ProductAttributeValueTag
    {

    }

    public class ProductAttributeValueTagValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }
        
        [Display(Name = "Идентификатор")]
        [Required(ErrorMessage = "Введите Идентификатор")]
        public string Name { get; set; }

        [Display(Name = "Доступность на текущем языке")]
        public bool IsCorrectLang { get; set; }

    }
}
