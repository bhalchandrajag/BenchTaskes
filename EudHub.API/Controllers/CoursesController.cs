using EduHub.API.Models;
using EduHub.API.Repository;
using EduHub.API.Services;
using EudHub.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EudHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

        private readonly ICourseService _courseRepository;
        private readonly EduHubInfoContext _eduHubInfoContext;

        public CoursesController(ICourseService courseRepository, EduHubInfoContext eduHubInfoContext)
        {
            _courseRepository = courseRepository;
            _eduHubInfoContext = eduHubInfoContext;
        }

        //[Authorize(Roles = "Student")]
        [HttpGet]
        [Route("GetAllCourses")]
        public async Task<IActionResult> GetAllCourse()
        {
            try
            {
                var user = await _courseRepository.GetAllCourseAsync();
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
       
        //[Authorize(Roles = "Student")]
        [HttpGet]
        [Route("GetCourseDetails")]
        public async Task<IActionResult> GetCourseDetails(int id)
        {
            try
            {
                var user = await _courseRepository.GetCourseDetailsAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[Authorize(Roles = "Educator")]
        [HttpPost]
        [Route("RegisterCourse")]
        public async Task<IActionResult> RegisterCourse([FromBody]Course registernewCourse)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); 
                }

                var maxCourseId = _courseRepository.GetusermaxId();

                
                var newCourseId = maxCourseId + 1;

              
                var newCourse = new Course
                {
                    courseId = newCourseId,
                    title= registernewCourse.title,
                    description = registernewCourse.description,
                    courseStartDate = registernewCourse.courseStartDate,
                    courseEndDate = registernewCourse.courseEndDate,
                    category = registernewCourse.category,
                    level = registernewCourse.level,
                    userId  = registernewCourse.userId,
                };

                await _courseRepository.RegisterCourseAsync(newCourse);
               
                return Ok("Course registered successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Educator")]
        [HttpPut]
        [Route("UpdateCourse")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course updatedCourse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
               // course.courseId = id;
                var updatedNewCourse = await _courseRepository.UpdateCourseAsync(id, updatedCourse);
                return Ok(updatedNewCourse);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        //[Authorize(Roles = "Educator")]
        [HttpDelete]
        [Route("DeleteCourse")]
        public async Task<IActionResult> DeleteCourse(int id)
        {

            try
            {
                await _courseRepository.DeleteCourseAsync(id);
                return Ok("Course deleted successfully"); 
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("Getcourseaddedbyedu")]
        public  ActionResult<IEnumerable<Course>> GetCourseaddedbyedu(int id)
        {
            // return await _userInfoContext.database.executesqlrawasync("");
            //var result1 = _userInfoContext.database.executesqlrawasync("select u.firstname,u.lastname,u.gender,u.email,u.mobilenumber,c.title,c.level\r\nfrom users u\r\ninner join courses c\r\non u.userid=c.userid\r\nwhere u.role='educator'");

            var result =  _eduHubInfoContext.Courses.FromSqlInterpolated($"dbo.GetcoursesListByEdu {id}");

            return Ok(result);

        }

    }
}
