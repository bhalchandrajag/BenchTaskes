namespace EudHub.API.PasswordHashing
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string passwordHash,string inputPassword);
    }
}