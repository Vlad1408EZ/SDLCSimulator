using System.Collections.Generic;
using SDLCSimulator_BusinessLogic.Models.Input;

namespace SDLCSimulator_Common.Fixtures
{
    public static class TaskForTeacherFilterInputFixture
    {
        public static TaskForTeacherFilterInput CreateValidEntity()
        {
            return new ()
            {
                FirstName = "Іван",
                LastName = "Іванов",
                Topic = "Вимоги",
                GroupNames = new List<string> {"ПЗ-41"}
            };
        }
    }
}
