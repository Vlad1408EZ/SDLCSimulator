using SDLCSimulator_Data.JsonTaskModels;

namespace SDLCSimulator_BusinessLogic.Models.Output
{
    public class StudentTaskResultOutputModel
    {
        public int Id { get; set; }
        public decimal Percentage { get; set; }
        public decimal FinalMark { get; set; }
        public int ErrorCount { get; set; }
        public string Result { get; set; }
    }
}
