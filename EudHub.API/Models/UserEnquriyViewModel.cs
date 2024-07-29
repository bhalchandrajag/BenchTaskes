using System.ComponentModel.DataAnnotations;

namespace EudHub.API.Models
{
    public class UserEnquriyViewModel
    {
        [Key]
        public int enquiryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string? URL { get; set; }
        public DateTime enquiryDate { get; set; }
        public string response { get; set; }
        public string status { get; set; }
        public int courseId { get; set; }
        public int userId { get; set; }


    }
}

