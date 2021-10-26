using System.Collections.Generic;

namespace SDLCSimulator_BusinessLogic.Models.Input
{
    public class TaskForTeacherFilterInput
    {
        public List<string> GroupNames { get; set; }
        public string Topic { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
