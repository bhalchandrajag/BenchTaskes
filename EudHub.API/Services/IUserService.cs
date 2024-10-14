

using EduHub.API.Models;
using EudHub.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> RegisterUserAsync( User user);
        Task<User> UserExist(string email,string username);
        Task<int> GetusermaxId();
        // void Save();ing
        Task<User> GetUserById(int id);
        Task<UpdateUserVM> UpdateUserAsync(int id, UpdateUserVM user);
        Task<User> DeleteUserAsync(int id);
        



    }
}
