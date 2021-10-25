using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BrunoZell.ModelBinding;
using SDLCSimulator_Data.Enums;

namespace SDLCSimulator_BusinessLogic.Models.Input
{
    public class TaskForStudentFilterInput
    {
        public List<DifficultyEnum> Difficulties { get; set; }
        public List<TaskTypeEnum> Types { get; set; }
        public string Topic { get; set; }
    }
}
