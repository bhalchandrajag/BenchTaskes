using BenchTask.API.Models;
using EudHub.API.Models;
using EudHub.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EudHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiresController : ControllerBase
    {

        private readonly IEnquriyService _enquriyService;
        private readonly EduHubInfoContext _eduHubInfoContext;

        public EnquiresController(IEnquriyService enquriyService, EduHubInfoContext eduhubInfoContext)
        {
            _enquriyService = enquriyService;
            _eduHubInfoContext = eduhubInfoContext;
        }

        [HttpGet("GetEnquries")]
        public async Task<IActionResult> GetEnquries()
        {
            try
            {
                var allEnquires = await _enquriyService.GetEnquiriesAsync();
                return Ok(allEnquires);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"an error occurred while retrieving users: {ex.Message}");
            }
        }

        [HttpGet("GetEnquriyByStudent")]
        public  ActionResult<IEnumerable<UserEnquriyViewModel>> GetEnquriyByStudent(int id)
        {
            try
            {
                var result = _eduHubInfoContext.UserEnquriyViewModels.FromSqlInterpolated($"dbo.sp_EnquiryDetails {id}");

               // var result = _eduHubInfoContext.UserEnquriyViewModels.FromSqlRaw("exec sp_EnquiryDetails");

                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"an error occurred while retrieving users: {ex.Message}");
            }
        }



        [HttpPost("SaveEnquries")]
        public async Task<IActionResult> SaveEnquries([FromBody]Enquiry newEnquries)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var maxEnquiresId = _enquriyService.GetusermaxId();

                // Increment maxUserId by 1 to generate a new UserId
                var newEnquiryId = maxEnquiresId + 1;


                var addNewEnquiry = new Enquiry
                {
                  enquiryId = newEnquiryId,
                  message=newEnquries.message,
                  response=newEnquries.response,
                  enquiryDate=newEnquries.enquiryDate,
                  status=newEnquries.status,
                  subject=newEnquries.subject,
                  courseId=newEnquries.courseId,
                  userId=newEnquries.userId,
                  URL=newEnquries.URL
                  
                };

                await _enquriyService.AddEnquiryAsync(addNewEnquiry);

                return Ok("Enquiry registered successfully");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }
    }
}
