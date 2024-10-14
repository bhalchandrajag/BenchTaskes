namespace EudHubConsume.MVC.Models
{
    public class UpdateUserViewModel
    {
        public string? Gender { get; set; }
        public string? Role { get; set; }
        // [StringLength(10)]
        public string? Mobilenumber { get; set; }


        public string? ProfileImage { get; set; }
    }
}
