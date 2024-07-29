using BenchTask.API.Models;
using BenchTask.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BenchTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly EduHubInfoContext _userInfoContext;
        private readonly IAuthenticationService _authentication;


        public AuthenticationController(EduHubInfoContext userInfoContext, IAuthenticationService authentication)
        {
            _userInfoContext = userInfoContext;
            _authentication = authentication;
        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<IActionResult> UserLogin(LoginViewModel user)
        {
            IActionResult response = Unauthorized();
            var validateUser = await _userInfoContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password);
            if (validateUser != null)
            {
                var token = _authentication.GenerateToken(validateUser);
                return Ok(new { token = token });
            }

            return response;


        }
    }
}
