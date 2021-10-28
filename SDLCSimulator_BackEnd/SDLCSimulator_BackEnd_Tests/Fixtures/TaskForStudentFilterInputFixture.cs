using System.Collections.Generic;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BackEnd_Tests.Fixtures
{
    public static class TaskForStudentFilterInputFixture
    {
        public static TaskForStudentFilterInput CreateValidInput()
        {
            return new()
            {
                Difficulties = new List<DifficultyEnum> {DifficultyEnum.Easy},
                Types = new List<TaskTypeEnum> {TaskTypeEnum.DragAndDrop},
                Topic = "Вимоги"
            };
        }
    }
}
