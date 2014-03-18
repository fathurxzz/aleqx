using System.ComponentModel.DataAnnotations;

namespace Mayka.Models
{
    [MetadataType(typeof(PhotoGalleryItemValidation))]
    public partial class PhotoGalleryItem
    {
         
    }

    public class PhotoGalleryItemValidation
    {
        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Изображение")]
        [Required]
        public string ImageSource { get; set; }

        [Display(Name = "Адрес страницы в интернете")]
        public string Url { get; set; }
    }
}