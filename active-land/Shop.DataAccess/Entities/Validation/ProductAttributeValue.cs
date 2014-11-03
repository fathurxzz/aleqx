using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities
{
    [MetadataType(typeof(ProductAttributeValueValidation))]
    partial class ProductAttributeValue
    {

    }

    public class ProductAttributeValueValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Доступность на текущем языке")]
        public bool IsCorrectLang { get; set; }

        
    }


}
