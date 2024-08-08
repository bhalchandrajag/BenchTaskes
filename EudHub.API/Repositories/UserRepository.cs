using BenchTask.API.Models;
using BenchTask.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace BenchTask.API.Repository
{
    public class UserRepository : IUserService
    {

        private readonly EduHubInfoContext _userInfoContext;

        public IUserService Object { get; }

        public UserRepository(EduHubInfoContext userInfoContext)
        {
            _userInfoContext = userInfoContext;
        }

       

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userInfoContext.Users.ToListAsync();
        }

     

        public User GetUserByUsernameAndPassword(string email, string username)
        {
            return _userInfoContext.Users.FirstOrDefault(x => x.Email == email && x.Username == username);
        }

        public int GetusermaxId()
        {
            return _userInfoContext.Users.Max(u => (int?)u.UserId) ?? 0;
        }


        public async Task<User> RegisterUserAsync(User registerUser)
        {

            await _userInfoContext.Users.AddAsync(registerUser);
            await _userInfoContext.SaveChangesAsync();
            return registerUser;

        }

        public async Task<User> UpdateUserAsync(int id, User updatedUser)
        {
            var oldUser = await _userInfoContext.Users.FirstOrDefaultAsync(s => s.UserId == id);
            if (oldUser == null)
            {
                throw new ArgumentException("User not found");
            }

            oldUser.Mobilenumber = updatedUser.Mobilenumber;
            oldUser.Role = updatedUser.Role;
            oldUser.ProfileImage = updatedUser.ProfileImage;
            oldUser.Username = updatedUser.Username;
            //_userInfoContext.Entry(oldUser).State = EntityState.Modified;

            await _userInfoContext.SaveChangesAsync();
            return updatedUser;
        }

        public async Task<User> DeleteUserAsync(int id)
        {

            var user = await _userInfoContext.Users.FirstOrDefaultAsync(s => s.UserId == id && s.Role == "Student");
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            _userInfoContext.Users.Remove(user);
            await _userInfoContext.SaveChangesAsync();

            return user;
        }

        public User GetUserById(int id)
        {
            return _userInfoContext.Users.Where(x => x.UserId == id).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            return _userInfoContext.Users.Where(x => x.Email ==email).FirstOrDefault();
        }



    }
}


