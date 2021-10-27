using System.Collections.Generic;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BusinessLogic.Models.Output
{
    public class UserOutputModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
        public List<string> Groups { get; set; }
    }
}
