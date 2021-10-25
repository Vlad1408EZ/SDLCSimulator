using System.ComponentModel.DataAnnotations.Schema;

namespace SDLCSimulator_Data
{
    [Table(nameof(GroupTask))]
    public class GroupTask
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
