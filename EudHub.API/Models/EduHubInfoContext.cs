using EudHub.API.Models;
using Microsoft.EntityFrameworkCore;


namespace EduHub.API.Models
{
    public partial class EduHubInfoContext : DbContext
    {
        public EduHubInfoContext(DbContextOptions<EduHubInfoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enquiry> Enquiries { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Material> Materials { get; set; }

        public virtual DbSet<LoginViewModel> LoginViewModels { get; set; }
       public virtual DbSet<UserFeedbackViewModel> UserFeedbackViewModels { get; set; }
        public virtual DbSet<UserEnquriyViewModel> UserEnquriyViewModels { get; set; }


    }
}
