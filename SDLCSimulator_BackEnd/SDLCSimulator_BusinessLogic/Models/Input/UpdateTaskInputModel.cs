using System.Collections.Generic;
using SDLCSimulator_Data.Enums;
using SDLCSimulator_Data.JsonTaskModels;

namespace SDLCSimulator_BusinessLogic.Models.Input
{
    public class UpdateTaskInputModel
    {
        public int TaskId { get; set; }
        public TaskTypeEnum Type { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public string Topic { get; set; }
        public DescriptionDragAndDropModel Description { get; set; }
        public StandardAndResultDragAndDropModel Standard { get; set; }
        public List<string> GroupNames { get; set; }
    }
}
