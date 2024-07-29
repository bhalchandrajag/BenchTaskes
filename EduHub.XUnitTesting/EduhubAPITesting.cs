using BenchTask.API.Controllers;
using BenchTask.API.Models;
using BenchTask.API.Repository;
using BenchTask.API.Services;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EduHub.XUnitTesting
{
    public class EduhubAPITesting
    {

        private readonly Mock<IUserService> _mockUserService;
       // private readonly AuthenticationController _authService;

        public EduhubAPITesting()
        {
            _mockUserService = new Mock<IUserService>();
            //_authService = new AuthenticationController(_mockUserService.Object);
        }


        [Fact]
        public void GetUserList()
        {
            var usersinfo = GetUsersData();
            _mockUserService.Setup(x => x.GetAllUsersAsync()).ReturnsAsync(usersinfo);

            var userdusersControllerata = new UsersController(_mockUserService.Object);

            var usersList  = userdusersControllerata.GetAllUsers();

            //Assert.NotNull(usersList);
            //Assert.Equal(GetUsersData().Count(),usersList.Res));
            //Assert.Equal(GetUsersData().ToString(), usersList.ToString());
            Assert.True(usersinfo.Equals(usersList));
        }


        [Fact]
        public  void AddUser_user()
        {
            //arrange
            var userList = GetUsersData();
            _mockUserService.Setup(x => x.RegisterUserAsync(userList[0])).ReturnsAsync(userList[0]);
               // .Returns(productList[1].ToString);
            var userdusersControllerata = new UsersController(_mockUserService.Object);
            //act
            var productResult = userdusersControllerata.RegisterUser(userList[0]);
            //assert
            Assert.NotNull(productResult);
            Assert.Equal(userList[0].UserId, productResult.Id);
            //Assert.True(userList[1].UserId == productResult.Id);
        }
        [Fact]
        public void CheckUserExistOrNotByUserName()
        {
            var user = new User { FirstName = "Prasanna", Email = string.Empty };
            var service = new UsersController(_mockUserService.Object);

            // Act
            var act = () => { service.RegisterUser(user); };


            // Assert
            var exception = Assert.Throws<InvalidDataException>(act);
            Assert.Equal("Email cannot be empty", exception?.Message);

        }

        private List<User> GetUsersData()
        {
            List<User> userData = new List<User>
        {
            new User
            {
                    UserId = 1,
                    Email = "p@p.com",
                    Password = "1234",
                    FirstName = "Bhalchandra",
                    LastName = "Jagdale",
                    Username = "Bhala",
                    Gender = "Male",
                    Role = "Educator",
                    Mobilenumber = "7588504907",
                    ProfileImage = "NA"
            },

            new User
            {
                 UserId = 2,
                    Email = "pm@p.com",
                    Password = "12344",
                    FirstName = "Father",
                    LastName = "Jagdale",
                    Username = "father",
                    Gender = "Male",
                    Role = "Educator",
                    Mobilenumber = "8149072204",
                    ProfileImage = "NA"
            }
        };
            return userData;
        }
    }
}