using System;
using System.Collections.Generic;
using System.Linq;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class DifficultyService : IDifficultyService
    {
        public List<string> GetAllDifficulties()
        {
            var values = Enum.GetValues<DifficultyEnum>().ToList();

            return values.Select(v => v.ToString()).ToList();
        }
    }
}
