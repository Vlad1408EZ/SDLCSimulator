using System.ComponentModel.DataAnnotations.Schema;

namespace SDLCSimulator_Data
{
    [Table(nameof(TaskResult))]
    public class TaskResult
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(4,2)")]
        public decimal Percentage { get; set; }
        [Column(TypeName = "decimal(4,2)")]
        public decimal FinalMark { get; set; }
        public int ErrorCount { get; set; }
        public string Result { get; set; }
        public int StudentId { get; set; }
        public User Student { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
