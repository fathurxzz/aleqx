using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.Entities.Validation
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
    }
}
