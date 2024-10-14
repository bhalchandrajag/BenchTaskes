using System.ComponentModel.DataAnnotations;

namespace EudHubConsume.MVC.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
