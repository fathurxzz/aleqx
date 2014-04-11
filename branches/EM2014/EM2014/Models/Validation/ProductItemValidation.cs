using System.ComponentModel.DataAnnotations;

namespace EM2014.Models
{
    [MetadataType(typeof(ProductItemValidation))]
    public partial class ProductItem
    {

    }

    public class ProductItemValidation
    {
        [Display(Name = "Описание")]
        public string Text { get; set; }

        [Display(Name = "Превьюшка 720х...")]
        public string ImageSource { get; set; }

        [Display(Name = "Порядок отображения")]
        [Required(ErrorMessage = "Введите порядок отображения")]
        public int SortOrder { get; set; }
    }
}