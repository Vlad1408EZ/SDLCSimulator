using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_Data;
using System.Collections.Generic;

namespace SDLCSimulator_BusinessLogic.Interfaces
{
    public interface IGradeCalculator
    {
        TaskResult CalculateTaskResult(Dictionary<string, List<string>> standard, CreateTaskResultInput input, int userId, Task task);
    }
}
