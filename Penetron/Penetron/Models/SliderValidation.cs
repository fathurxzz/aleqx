using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Penetron.Models
{
    [MetadataType(typeof(SliderValidation))]
    public partial class Slider
    {

    }

    public class SliderValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Адрес сраницы (относительно домена)")]
        public string Url { get; set; }

        [DisplayName("Изображение")]
        public string ImageSource { get; set; }
    }
}