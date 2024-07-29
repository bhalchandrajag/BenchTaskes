using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenchTask.API.Models
{
    [NotMapped]
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The Email is required")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
