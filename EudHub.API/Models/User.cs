using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EduHub.API.Models
{
    public class User
    {

        [Key]
        public int? UserId { get; set; }

       // [Required(ErrorMessage = "The FirstName is required")]
        public string? FirstName { get; set; }

       // [Required(ErrorMessage = "The LastName is required")]
        public string? LastName { get; set; }

       // [Required(ErrorMessage = "The Gender is required")]
        public string? Gender { get; set; }

       // [Required(ErrorMessage = "The Username is required")]
        public string? Username { get; set; }

        //[Required]
        //[StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$")]
        //[DataType(DataType.Password)]
        //[JsonIgnore]
        public string? Password { get; set; }


        public string? Role { get; set; }

        //[Required(ErrorMessage = "The email address is required")]
      //  [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string? Email { get; set; }


       // [StringLength(10)]
        public string? Mobilenumber { get; set; }


        public string? ProfileImage { get; set; }

    }
}


