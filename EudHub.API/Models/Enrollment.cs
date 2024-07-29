using System.ComponentModel.DataAnnotations;

namespace EudHub.API.Models
{
    public class Enrollment
    {
        [Key]
        public int enrollmentId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime enrollmentDate { get; set; }

        [Required(ErrorMessage = "The status is required")]
        public string status { get; set; }
        public int userId { get; set; }
        public int courseId { get; set; }
       


        
    }
}
