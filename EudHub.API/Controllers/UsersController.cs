using BenchTask.API.Models;
using BenchTask.API.Repository;
using BenchTask.API.Services;
using EudHub.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BenchTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userRepository;
        

        public UsersController(IUserService userRepository)
        {
            _userRepository = userRepository;
            

        }

        //[Authorize(Roles = "Educator")]
        [HttpGet]
        [Route("GetAllUser")]
        public async Task<IActionResult> GetAllUsers()
        {

            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"an error occurred while retrieving users: {ex.Message}");
            }

        }

        [HttpGet("getproductbyid")]
        public User GetUserById(int Id)
        {
            return _userRepository.GetUserById(Id);
        }

        // [Authorize]
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User registerUser)
        {
            try {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var maxUserId = _userRepository.GetusermaxId();

                // Increment maxUserId by 1 to generate a new UserId
                var newUserId = maxUserId + 1;

                // Check if username already exists
                var userExists = _userRepository.GetUserByUsernameAndPassword(registerUser.Email, registerUser.Username);
                if (userExists != null)
                {
                    return BadRequest("User already exists");
                }


                var newUser = new User
                {
                    UserId = newUserId,
                    Email = registerUser.Email,
                    Password = registerUser.Password,
                    FirstName = registerUser.FirstName,
                    LastName = registerUser.LastName,
                    Username = registerUser.Username,
                    Gender = registerUser.Gender,
                    Role = registerUser.Role,
                    Mobilenumber = registerUser.Mobilenumber,
                    ProfileImage = registerUser.ProfileImage,
                };

                await _userRepository.RegisterUserAsync(newUser);
               
                return Ok("User registered successfully");
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }


        // [Route("{id}")]
       // [Authorize(Roles = "Educator, Student")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var updatedNewUser = await _userRepository.UpdateUserAsync(id, updatedUser);
                return Ok(updatedNewUser);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }


        //[Authorize(Roles ="Educator")]
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);
                return Ok("User deleted successfully"); 
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    } 
}


