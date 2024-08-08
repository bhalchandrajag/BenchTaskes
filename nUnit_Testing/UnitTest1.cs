using BenchTask.API.Controllers;
using BenchTask.API.Models;
using BenchTask.API.Repository;
using BenchTask.API.Services;
using Castle.Core.Configuration;
using Castle.Core.Resource;
using EudHub.API.PasswordHashing;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace nUnit_Testing
{
    public class Tests
    {
        //AuthenticationController obj;

        //private Mock<IUserService> _UserService;
        private Mock<AuthenticationRepository> _authentication;
        private readonly Mock<EduHubInfoContext> _userInfoContext;
        private Mock<IPasswordHasher> _passwordHasher;
        private Mock<IConfiguration> _config;

        public Tests(Mock<AuthenticationRepository> authentication, Mock<IConfiguration> config,
            Mock<IPasswordHasher> passwordHasher, Mock<EduHubInfoContext> userInfoContext)
        {
            _authentication=authentication;
            _passwordHasher=passwordHasher;
            _userInfoContext=userInfoContext;
            _config=config;
        }

        //public void Setup()
        //{

        //}

        [TearDown]
        public void destroy()
        {
            _authentication = null;
        }

        [Test]
        public void GenerateToken_ShouldReturnValidToken()
        {
            string tokens="";
            //User user = new User
            //{
            //    Email= "test",
            //};
            // Arrange
            var mockUserService = new Mock<IAuthenticationService>();

            mockUserService.Setup(s => s.GetUserById(It.IsAny<int>())).Returns(new User { UserId = 1, Email = "papanjag@gmail.com" });
           var token= mockUserService.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns(tokens);
           // var tokengenrator= new AuthenticationRepository(mockUserService.Object);

            // Act
            //var token = tokenGenerator.GenerateToken(1);

            // Assert
            Assert.NotNull(token);

            // Use IdentityModel or custom logic to validate token structure and claims
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokens);

            Assert.AreEqual("Admin", jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value);
            // Add more assertions for other claims and token structure

        }
    }

    
}