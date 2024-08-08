using System.ComponentModel.DataAnnotations;

namespace EudHubConsume.MVC.Models
{
    public class CourseViewModel
    {
        [Key]
        public int courseId { get; set; }

        [Required(ErrorMessage = "The title is required")]
        public string title { get; set; }

        public string description { get; set; }

        //[DataType(DataType.DateTime)]
        public DateTime courseStartDate { get; set; }

        // [DataType(DataType.DateTime)]
        public DateTime courseEndDate { get; set; }

        public string category { get; set; }

        [Required(ErrorMessage = "The Level is required")]
        // [JsonConverter(typeof(JsonStringEnumConverter))]
        public string? level { get; set; }

        [Required(ErrorMessage = "The userId is required")]
        public int userId { get; set; }
    }
}
