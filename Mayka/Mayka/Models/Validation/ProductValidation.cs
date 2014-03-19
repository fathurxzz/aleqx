using System.ComponentModel.DataAnnotations;

namespace Mayka.Models
{
    [MetadataType(typeof(ProductValidation))]
    public partial class Product
    {


    }

    public class ProductValidation
    {
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Превьюшка")]
        public string PreviewImageSource { get; set; }

        [Display(Name = "Порядок отображения")]
        [Required(ErrorMessage = "Введите порядок отображения")]
        public int SortOrder { get; set; }
    }
}