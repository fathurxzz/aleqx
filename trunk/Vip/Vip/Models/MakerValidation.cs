using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Vip.Models
{
    [MetadataType(typeof(MakerValidation))]
    public partial class Maker
    {

    }

    public class MakerValidation
    {
        [Required(ErrorMessage = "Обязательно!")]
        [DisplayName("Заголовок")]
        public string Title { get; set; }
    }
}