using BenchTask.API.Controllers;
using BenchTask.API.Models;
using BenchTask.API.Repository;
using BenchTask.API.Services;
using Castle.Core.Resource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace EduHub.XUnitTesting
{
    public class EduhubAPITesting
    {

        private Mock<IUserService> _mockUserService;


        public EduhubAPITesting()
        {
            _mockUserService = new Mock<IUserService>();

        }



        //[Fact]
        //public void  SaveUser_ShouldThrowException_WhenUserIsNull()
        //{
        //    // Arrange
        //    var service = new UserRepository();

        //    // Act
        //    var act = () => { service.RegisterUserAsync(null); };


        //    // Assert
        //    Assert.Throws<ArgumentNullException>(act);
        //}

        [Fact]
        public void AgeMustBeWithinRange()
        {
            // Arrange
            var model = new User {Mobilenumber="1234567" };

            // Act
            var context = new ValidationContext(model);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(model, context, results,
 true);

            // Assert
            Assert.False(isValid);
            Assert.Single(results);
            Assert.Equal("The field Age must be between 18 and 120.", results[0].ErrorMessage);
        }
    }






    //private List<User> GetUsersData()
    //{
    //    List<User> userData = new List<User>
    //    {
    //        new User
    //        {
    //                UserId = 1,
    //                Email = "p@p.com",
    //                Password = "1234",
    //                FirstName = "Bhalchandra",
    //                LastName = "Jagdale",
    //                Username = "Bhala",
    //                Gender = "Male",
    //                Role = "Educator",
    //                Mobilenumber = "7588504907",
    //                ProfileImage = "NA"
    //        },

    //        new User
    //        {
    //             UserId = 2,
    //                Email = "pm@p.com",
    //                Password = "12344",
    //                FirstName = "Father",
    //                LastName = "Jagdale",
    //                Username = "father",
    //                Gender = "Male",
    //                Role = "Educator",
    //                Mobilenumber = "8149072204",
    //                ProfileImage = "NA"
    //        }
    //    };
    //    return userData;
    //}
}
