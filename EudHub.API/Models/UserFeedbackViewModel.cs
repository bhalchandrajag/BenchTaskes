using System.ComponentModel.DataAnnotations;

namespace EudHub.API.Models
{
    
    public class UserFeedbackViewModel
    {
        [Key]
        public int feedbackId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string feedback { get; set; }
        public DateTime feedbackDate { get; set; }
        public string courseTitle { get; set; }
        public string courseLevel { get; set; }
        public int courseId { get; set; }
        public int userId { get; set; }


    }
}
