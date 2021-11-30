using System.Collections.Generic;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BusinessLogic.Models.Output
{
    public class TeacherTasksOutputModel
    {
        public int Id { get; set; }
        public TaskTypeEnum Type { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public int MaxGrade { get; set; }
        public int TaskTime { get; set; }
        public decimal ErrorRate { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string Standard { get; set; }

        public List<GroupOutputModel> Groups { get; set; }

        public List<TeacherTaskResultOutputModel> TeachersTaskResults { get; set; } =
            new();
    }
}
