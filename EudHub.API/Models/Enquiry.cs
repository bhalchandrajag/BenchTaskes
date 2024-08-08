using System.ComponentModel.DataAnnotations;

namespace EudHub.API.Models
{
    public class Enquiry
    {
        [Key]
        public int enquiryId { get; set; }

        
        [Required(ErrorMessage = "The title is required")]
        public string subject { get; set; }

        [Required(ErrorMessage = "The message is required")]
        public string message { get; set; }
        public string response { get; set; }
        public string? URL {  get; set; }

        [DataType(DataType.DateTime)]
        public DateTime enquiryDate { get; set; }

        [Required(ErrorMessage = "The status is required")]
        public string status { get; set; }
        public int userId { get; set; }
        public int courseId { get; set; }

    }
}
