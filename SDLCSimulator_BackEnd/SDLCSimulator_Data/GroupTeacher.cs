using System.ComponentModel.DataAnnotations.Schema;

namespace SDLCSimulator_Data
{
    [Table(nameof(GroupTeacher))]
    public class GroupTeacher
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int TeacherId { get; set; }
        public User Teacher { get; set; }
    }
}
