using System;
using System.Collections.Generic;
using System.Linq;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class TypeService : ITypeService
    {
        public List<TaskTypeEnum> GetAllTaskTypes()
        {
            var values = Enum.GetValues<TaskTypeEnum>().ToList();

            return values;
        }
    }
}
