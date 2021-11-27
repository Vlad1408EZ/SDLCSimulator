using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_Data
{
    [Table(nameof(User))]
    public class User
    {
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; }
        public int? GroupId { get; set; }
        public Group Group { get; set; }

        public ICollection<Task> Tasks { get; set; } = new List<Task>();
        public ICollection<TaskResult> TaskResults { get; set; } = new List<TaskResult>();
        public ICollection<GroupTeacher> GroupTeachers { get; set; } = new List<GroupTeacher>();
    }
}
