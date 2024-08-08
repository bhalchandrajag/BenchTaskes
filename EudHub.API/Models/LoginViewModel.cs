using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenchTask.API.Models
{
    [NotMapped]
    public class LoginViewModel
    {
        
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
