namespace EudHub.API.Models
{
    public class UpdateUserVM
    {
        public string? Gender { get; set; }
        public string? Role { get; set; }
        // [StringLength(10)]
        public string? Mobilenumber { get; set; }


        public string? ProfileImage { get; set; }
    }
}
