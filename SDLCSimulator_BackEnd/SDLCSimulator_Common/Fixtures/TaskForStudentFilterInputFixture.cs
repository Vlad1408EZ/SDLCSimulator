using System.Collections.Generic;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_Common.Fixtures
{
    public static class TaskForStudentFilterInputFixture
    {
        public static TaskForStudentFilterInput CreateValidInput()
        {
            return new()
            {
                Difficulties = new List<DifficultyEnum> {DifficultyEnum.Medium},
                Types = new List<TaskTypeEnum> {TaskTypeEnum.RequirementsTypeAndOrderByImportance},
                Topic = "Вимоги"
            };
        }
    }
}
