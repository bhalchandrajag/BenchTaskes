using BenchTask.API.Models;


namespace BenchTask.API.Services
{
    public interface IAuthenticationService
    {
        string GenerateToken(User users);
        //void ValidateUser(string v1, string v2);
        //Task Login(User users);
    }
}
