using System;
using System.Collections.Generic;
using System.Linq;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class RoleService : IRoleService
    {
        public List<RoleEnum> GetAllRoles()
        {
            var values = Enum.GetValues<RoleEnum>().ToList();

            return values;
        }
    }
}
