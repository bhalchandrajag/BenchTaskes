using EduHub.API.Models;
using EduHub.API.Services;
using EudHub.API.PasswordHashing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EduHub.API.Repository
{
    public class AuthenticationRepository : IAuthenticationService
    {
        private IConfiguration _config;
        private readonly EduHubInfoContext _userInfoContext;
        private readonly IPasswordHasher _passwordHasher;
        public AuthenticationRepository(IConfiguration configuration,
            EduHubInfoContext userInfoContext, IPasswordHasher passwordHasher)
        {
            _config = configuration;
            _userInfoContext = userInfoContext;
            _passwordHasher = passwordHasher;
        }

        public string GenerateToken(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );


            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
        public User GetUserById(int id)
        {
            return _userInfoContext.Users.Where(x => x.UserId == id).FirstOrDefault();
        }

    }
}

