using EduHub.API.Models;
using EduHub.API.Services;
using EudHub.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace EduHub.API.Repository
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

     

        public async Task<User> UserExist(string email, string username)
        {
            return await _userInfoContext.Users.FirstOrDefaultAsync(x => x.Email == email || x.Username == username);
        }

        public async Task<int> GetusermaxId()
        {
            return await _userInfoContext.Users.MaxAsync(u => (int?)u.UserId) ?? 0;
        }


        public async Task<User> RegisterUserAsync(User registerUser)
        {

            await _userInfoContext.Users.AddAsync(registerUser);
            await _userInfoContext.SaveChangesAsync();
            return registerUser;

        }

        public async Task<UpdateUserVM> UpdateUserAsync(int id, UpdateUserVM updatedUser)
        {
            var oldUser = _userInfoContext.Users.FirstOrDefault(s => s.UserId == id);
            if (oldUser == null)
            {
                throw new ArgumentException("User not found");
            }

            oldUser.Mobilenumber = updatedUser.Mobilenumber;
            oldUser.Role = updatedUser.Role;
            oldUser.ProfileImage = updatedUser.ProfileImage;
            oldUser.Gender = updatedUser.Gender;
            //_userInfoContext.Entry(oldUser).State = EntityState.Modified;

            await _userInfoContext.SaveChangesAsync();
            return updatedUser;
        }

        public async Task<User> DeleteUserAsync(int id)
        {

            var user = await _userInfoContext.Users.FirstOrDefaultAsync(s => s.UserId == id);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            _userInfoContext.Users.Remove(user);
            await _userInfoContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userInfoContext.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userInfoContext.Users.Where(x => x.Email ==email).FirstOrDefaultAsync();
        }

        
      
    }
}


