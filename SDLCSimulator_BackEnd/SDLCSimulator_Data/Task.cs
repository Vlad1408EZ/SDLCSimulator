using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_Data
{
    [Table(nameof(Task))]
    public class Task
    {
        public int Id { get; set; }
        public TaskTypeEnum Type { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public MaxGradeEnum MaxGrade { get; set; }
        public ErrorRateEnum ErrorRate { get; set; }
        [MaxLength(50)]
        public string Topic { get; set; }
        public string Description { get; set; }
        public string Standard { get; set; }

        public int TeacherId { get; set; }
        public User Teacher { get; set; }

        public ICollection<TaskResult> TaskResults { get; set; }
        public ICollection<GroupTask> GroupTasks { get; set; }
    }
}
