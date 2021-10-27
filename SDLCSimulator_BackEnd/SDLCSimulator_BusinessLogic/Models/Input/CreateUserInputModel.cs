using System.Collections.Generic;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BusinessLogic.Models.Input
{
    public class CreateUserInputModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public List<string> Groups { get; set; }
    }
}
