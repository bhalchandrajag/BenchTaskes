using EduHub.API.Models;
using EduHub.API.Repository;
using EudHub.API.Models;
using EudHub.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EudHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedBackService _feedbackService;
        private readonly EduHubInfoContext  _eduHubInfoContext;

        public FeedbacksController(IFeedBackService feedbackService, EduHubInfoContext eduHubInfoContext)
        {
            _feedbackService = feedbackService;
            _eduHubInfoContext = eduHubInfoContext;
        }


        [HttpGet("GetFeedbacks")]
        public async Task<IActionResult> GetAllFeedbacks() 
        {
            try
            {
                var allFeedbacks = await _feedbackService.GetFeedbackAsync();
                return Ok(allFeedbacks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"an error occurred while retrieving users: {ex.Message}");
            }
        }


        [HttpGet]
        [Route("GetStudentFeedbacks")]
        public ActionResult<IEnumerable<UserFeedbackViewModel>> GetStudentFeedbacks(int id)
        {
            // return await _userInfoContext.database.executesqlrawasync("");
            //var result1 = _userInfoContext.database.executesqlrawasync("select u.firstname,u.lastname,u.gender,u.email,u.mobilenumber,c.title,c.level\r\nfrom users u\r\ninner join courses c\r\non u.userid=c.userid\r\nwhere u.role='educator'");

            var result = _eduHubInfoContext.UserFeedbackViewModels.FromSqlInterpolated($"dbo.sp_ViewFeedback {id}");

            return Ok(result);

        }


        //[Authorize(Roles ="Student")]
        [HttpPost("Save")]
        public async Task<IActionResult> SaveFeedback([FromBody]Feedback newFeedback)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var maxFeedbackId = _feedbackService.GetusermaxId();

                // Increment maxUserId by 1 to generate a new UserId
                var newfeedbackId = maxFeedbackId + 1;


                var addNewfeedback = new Feedback
                {
                    feedbackId = newfeedbackId,
                    feedback=newFeedback.feedback,
                    date=newFeedback.date,
                    userId=newFeedback.userId,
                    courseId=newFeedback.courseId
                };

               await _feedbackService.SaveFeedback(addNewfeedback);

                return Ok("Feedback registered successfully");

            }
            catch(Exception ex) 
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }
    }
}
