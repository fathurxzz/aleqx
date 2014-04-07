using System.ComponentModel.DataAnnotations;

namespace EM2014.Models
{
    [MetadataType(typeof(ProductValidation))]
    public partial class Product
    {


    }

    public class ProductValidation
    {
        [Display(Name = "Адрес страницы")]
        public string Name { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Описание")]
        public string Text { get; set; }

        [Display(Name = "Превьюшка 305х172")]
        public string ImageSource { get; set; }

        //[Display(Name = "Превьюшка для айпада 228х228")]
        //public string PreviewImageSourceIpad { get; set; }

        [Display(Name = "Порядок отображения")]
        [Required(ErrorMessage = "Введите порядок отображения")]
        public int SortOrder { get; set; }
    }
}