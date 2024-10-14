using EduHub.API.Models;
using EduHub.API.Services;
using EudHub.API.Models;
using EudHub.API.PasswordHashing;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHub.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly EduHubInfoContext _userInfoContext;
        private readonly IAuthenticationService _authentication;
        private readonly IPasswordHasher _passwordHasher;

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


            try
            {
                UserDetailsVM user = new UserDetailsVM();
                IActionResult response = Unauthorized();

                var validateUser = await _userInfoContext.Users.FirstOrDefaultAsync(e => e.Email == userModel.Email);

                if (validateUser == null)
                {
                    return StatusCode(400, "Invalid Email!");
                }
                else
                {
                    var result = _passwordHasher.Verify(validateUser.Password, userModel.Password);

                    if (!result)
                    {
                        return StatusCode(400, "Invalid Password!");
                    }

                    var token = _authentication.GenerateToken(validateUser);

                    user.FirstName = validateUser.FirstName;
                    user.LastName=validateUser.LastName;
                    user.UserId= validateUser.UserId;
                    user.Token = token;

                    return Ok(user);
                } 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

    }
}
