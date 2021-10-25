using SDLCSimulator_Data;

namespace SDLCSimulator_BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        string GenerateWebTokenForUser(User user);
        string HashPassword(string password);
        bool VerifyPassword(string hashedPassword,string password);
    }
}
