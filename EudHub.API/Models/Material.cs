using System.ComponentModel.DataAnnotations;

namespace EudHub.API.Models
{
    public class Material
    {
        [Key]
        public int materialId { get; set; }

        [Required(ErrorMessage = "The title is required")]
        public string title { get; set; }
        public string description { get; set; }

        [Required(ErrorMessage = "The URL is required")]
        [DataType(DataType.Url)]
        public string URL { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime uploadDate { get; set; }

        public string contentType { get; set; }
        public int courseId { get; set; }

    }
}
