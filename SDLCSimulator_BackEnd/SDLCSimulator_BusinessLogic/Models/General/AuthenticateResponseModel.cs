using SDLCSimulator_Data;

namespace SDLCSimulator_BusinessLogic.Models.General
{
    public class AuthenticateResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }

        public AuthenticateResponseModel(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Role = user.Role.ToString();
            Email = user.Email;
            Token = token;
        }
        public AuthenticateResponseModel() { }
    }
}
