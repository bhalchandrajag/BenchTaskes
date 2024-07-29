

using BenchTask.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace BenchTask.API.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> RegisterUserAsync( User user);
        User GetUserByUsernameAndPassword(string email,string usename);
        int GetusermaxId();
        // void Save();
        public User GetUserById(int id);
        Task<User> UpdateUserAsync(int id,User user);
        Task<User> DeleteUserAsync(int id);
        



    }
}
