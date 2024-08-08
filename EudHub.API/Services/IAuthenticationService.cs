using BenchTask.API.Models;


namespace BenchTask.API.Services
{
    public interface IAuthenticationService
    {
        string GenerateToken(User users);
        User GetUserById(int id);
    }
}
