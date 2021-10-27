using System.Collections.Generic;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BusinessLogic.Interfaces
{
    public interface IDifficultyService
    {
        List<DifficultyEnum> GetAllDifficulties();
    }
}
