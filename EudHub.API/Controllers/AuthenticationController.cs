using BenchTask.API.Models;
using BenchTask.API.Services;
using EudHub.API.PasswordHashing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BenchTask.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly EduHubInfoContext _userInfoContext;
        private readonly IAuthenticationService _authentication;
        private readonly IPasswordHasher _passwordHasher ;

        public AuthenticationController(EduHubInfoContext userInfoContext, 
            IAuthenticationService authentication, IPasswordHasher passwordHasher)
        {
            _userInfoContext = userInfoContext;
            _authentication = authentication;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<IActionResult> UserLogin(LoginViewModel userModel)
        {
            string tokenstring=string.Empty;

            IActionResult response = Unauthorized();

            var validateUser = await _userInfoContext.Users.FirstOrDefaultAsync(e=>e.Email== userModel.Email);

            var result=_passwordHasher.Verify(validateUser.Password, userModel.Password);
            if (!result)
            {
                throw new Exception("Username and Password is not correct.");
            }

            if (validateUser != null)
            {
                var token = _authentication.GenerateToken(validateUser);
                return Ok(tokenstring = token);
            }

            return response;

          
        }



   

    }
}
