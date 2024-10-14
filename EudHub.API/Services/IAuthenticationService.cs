using EduHub.API.Models;


namespace EduHub.API.Services
{
    public interface IAuthenticationService
    {
        string GenerateToken(User users);
        User GetUserById(int id);
    }
}
