using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpaceGame.UI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

    }
}