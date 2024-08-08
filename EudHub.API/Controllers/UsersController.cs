using BenchTask.API.Models;
using BenchTask.API.Repository;
using BenchTask.API.Services;
using EudHub.API.PasswordHashing;
using EudHub.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BenchTask.API.Controllers
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


        //[Authorize(Roles ="Student")]
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
        public IActionResult GetUser(int Id)
        {
            try
            {
                var users = _userRepository.GetUserById(Id);
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

                var maxUserId = _userRepository.GetusermaxId();

                // Increment maxUserId by 1 to generate a new UserId
                var newUserId = maxUserId + 1;

                // Check if username already exists
                var userExists = _userRepository.GetUserByUsernameAndPassword(registerUser.Email, registerUser.Username);
                if (userExists != null)
                {
                    return BadRequest("User already exists");
                }
                var hashPassword = _passwordHasher.Hash(registerUser.Password);
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
        public async Task<IActionResult> UpdateUser(int id, User updatedUser)
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


        [HttpDelete("{id}")]
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


