using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EudHub.API.Models
{
    public class Feedback
    {
        [Key]
        public int feedbackId { get; set; }

        [Required(ErrorMessage = "The feedback is required")]
        public string feedback { get; set; }

       
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }
        public int userId { get; set; }

        public int courseId { get; set; }

    }
}
