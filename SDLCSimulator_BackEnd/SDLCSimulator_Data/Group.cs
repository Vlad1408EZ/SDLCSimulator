using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SDLCSimulator_Data
{
    [Table(nameof(Group))]
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public ICollection<GroupTask> GroupTasks { get; set; }
        public ICollection<GroupTeacher> GroupTeachers { get; set; }
    }
}
