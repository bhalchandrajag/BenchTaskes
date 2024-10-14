using EduHub.API.Models;

namespace EudHub.API.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllCourseAsync();
        Task<Course> RegisterCourseAsync(Course course);
        Task<Course> GetCourseDetailsAsync(int id);
        //User GetUserByUsernameAndPassword(string email, string usename);
        int GetusermaxId();
        // void Save();
       // public IEnumerable<Course> GetCourseAddedByEdu();
        Task<Course> UpdateCourseAsync(int id, Course course);
        Task<Course> DeleteCourseAsync(int id);
    }
}
