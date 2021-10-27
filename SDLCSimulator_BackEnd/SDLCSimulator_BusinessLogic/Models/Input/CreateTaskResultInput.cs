using SDLCSimulator_Data.JsonTaskModels;

namespace SDLCSimulator_BusinessLogic.Models.Input
{
    public class CreateTaskResultInput
    {
        public int TaskId { get; set; }
        public StandardAndResultDragAndDropModel Result { get; set; }
        public int ErrorCount { get; set; }
    }
}
