using System.Collections.Generic;

namespace SDLCSimulator_BusinessLogic.Models.General
{
    public class EmailModel
    {
        public List<int> GroupIds { get; set; }
        public int TeacherId { get; set; }
        public string Topic { get; set; }
    }
}
