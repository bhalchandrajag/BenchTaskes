using BenchTask.API.Models;
using EudHub.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EudHub.API.Repositories
{
    public class CourseRepository : ICourseService
    {
        private readonly EduHubInfoContext _userInfoContext;
       // private readonly DbContext _dbContext;

        public CourseRepository(EduHubInfoContext courseInfoContext)
        {
            _userInfoContext = courseInfoContext;
            //_dbContext = dbContext;
        }


        public async Task<IEnumerable<Course>> GetAllCourseAsync()
        {
            //string sttoredproc = "exec GetStudentList";

            var result = await _userInfoContext.Courses.ToListAsync();
            return result;
        }

       
        public async Task<Course> GetCourseDetailsAsync(int id)
        {
            return await _userInfoContext.Courses.FindAsync(id);
        }


        public async Task<Course> RegisterCourseAsync(Course registerCourse)
        {
            await _userInfoContext.Courses.AddAsync(registerCourse);
            await _userInfoContext.SaveChangesAsync();
            return registerCourse;
        }

        public async Task<Course> UpdateCourseAsync(int id, Course modifiedCourse)
        {
            var oldCourse = await _userInfoContext.Courses.FirstOrDefaultAsync(s => s.courseId == id);
            if (oldCourse == null)
            {
                throw new ArgumentException("User not found");
            }

            oldCourse.title = modifiedCourse.title;
            oldCourse.description = modifiedCourse.description;
            oldCourse.category = modifiedCourse.category;
            oldCourse.level = modifiedCourse.level;
            oldCourse.category = modifiedCourse.category;

            // _userInfoContext.Entry(modifiedCourse).State= EntityState.Modified; 
            await _userInfoContext.SaveChangesAsync();
            return modifiedCourse;
        }

        public async Task<Course> DeleteCourseAsync(int id)
        {

            var course = await _userInfoContext.Courses.FirstOrDefaultAsync(s => s.userId == id);
            if (course == null)
            {
                throw new ArgumentException("User not found");
            }

            _userInfoContext.Courses.Remove(course);
            await _userInfoContext.SaveChangesAsync();

            return course;
        }

        public int GetusermaxId()
        {
            return _userInfoContext.Courses.Max(u => (int?)u.courseId) ?? 0;
        }

        //public async Task<IEnumerable<Course>> Getcourseaddedbyedu(int id)
        //{
        //    // return await _userInfoContext.database.executesqlrawasync("");
        //    //var result1 = _userInfoContext.database.executesqlrawasync("select u.firstname,u.lastname,u.gender,u.email,u.mobilenumber,c.title,c.level\r\nfrom users u\r\ninner join courses c\r\non u.userid=c.userid\r\nwhere u.role='educator'");

        //    var result =await _userInfoContext.Courses.FromSqlRaw("exec GetcoursesListByEdu {id}");

        //    return result;

        //}
    }
}
