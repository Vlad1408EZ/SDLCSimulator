using System.Collections.Generic;
using SDLCSimulator_Data.Enums;
using SDLCSimulator_Data.JsonTaskModels;

namespace SDLCSimulator_BusinessLogic.Models.Output
{
    public class StudentTasksOutputModel
    {
        public int Id { get; set; }
        public TaskTypeEnum Type { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public int MaxGrade { get; set; }
        public decimal ErrorRate { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public string Standard { get; set; }
        public string TeacherFirstName { get; set; }
        public string TeacherLastName { get; set; }

        public List<StudentTaskResultOutputModel> StudentsTaskResults { get; set; } =
            new ();
    }
}
