namespace EudHubConsume.MVC.Models
{
    public class UserDetailsVM
    {
        public int? UserId { get; set; }

        // [Required(ErrorMessage = "The FirstName is required")]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Token { get; set; }
    }
}
