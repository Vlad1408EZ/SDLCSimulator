namespace SDLCSimulator_BusinessLogic.Models.Output
{
    public class TeacherTaskResultOutputModel
    {
        public int Id { get; set; }
        public decimal Percentage { get; set; }
        public decimal FinalMark { get; set; }
        public int ErrorCount { get; set; }
        public string Result { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string GroupName { get; set; }
    }
}
