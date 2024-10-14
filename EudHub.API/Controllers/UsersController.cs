using EduHub.API.Models;
using EduHub.API.Repository;
using EduHub.API.Services;
using EudHub.API.Models;
using EudHub.API.PasswordHashing;
using EudHub.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EduHub.API.Controllers
{
    [EnableCors]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userRepository;
        private readonly IPasswordHasher _passwordHasher;


        public UsersController(IUserService userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;

        }


      // [Authorize(Roles ="Educator")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {

            try
            {
                var users = await _userRepository.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(401, $"an error occurred while retrieving users: {ex.Message}");
            }

        }


        [HttpGet]
        public async Task<IActionResult> GetUser(int Id)
        {
            try
            {
                var users = await _userRepository.GetUserById(Id);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(401, $"an error occurred while retrieving users: {ex.Message}");
            }


        }

     
        [HttpPost]
        public async Task<IActionResult> RegisterUser(User registerUser)
        {
            try {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                int maxUserId = await _userRepository.GetusermaxId();

                // Increment maxUserId by 1 to generate a new UserId
                int newUserId = maxUserId + 1;

                if (registerUser.FirstName == registerUser.Username)
                {
                    return BadRequest("first name and username should not be same!");
                }
                // Check if username already exists
                var userExists = await _userRepository.UserExist(registerUser.Email, registerUser.Username);
                if (userExists!=null)
                {
                    return BadRequest("User already exists");
                }
                var hashPassword = _passwordHasher.Hash(registerUser.Password);
               // var hashPassword = BCrypt.Net.BCrypt.HashPassword(registerUser.Password);
                var newUser = new User
                {
                    UserId = newUserId,
                    Email = registerUser.Email,
                    Password = hashPassword,
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
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }


        
        [HttpPut]
       // [Authorize]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserVM updatedUser)
            {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {

                var q = await _userRepository.UpdateUserAsync(id, updatedUser);
                return Ok(q);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
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


