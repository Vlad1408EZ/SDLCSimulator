using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SDLCSimulator_BusinessLogic.Helpers;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;
using SDLCSimulator_Data;
using SDLCSimulator_Data.Enums;
using SDLCSimulator_Data.JsonTaskModels;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class TaskResultService : ITaskResultService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskResultRepository _taskResultRepository;
        private IGradeCalculator _gradeCalculator;

        public TaskResultService(ITaskRepository taskRepository, ITaskResultRepository taskResultRepository)
        {
            _taskRepository = taskRepository;
            _taskResultRepository = taskResultRepository;
        }

        private void SetGradeCalculator(IGradeCalculator gradeCalculator)
        {
            _gradeCalculator = gradeCalculator;
        }

        public async Task<StudentTaskResultOutputModel> SetTaskResultAsync(CreateTaskResultInput input, int userId)
        {
            var task = await _taskRepository.GetSingleByConditionAsync(t => t.Id == input.TaskId);
            if (task == null)
            {
                throw new InvalidOperationException($"Завдання з айді {input.TaskId} не знайдено");
            }
            var standard = JsonConvert.DeserializeObject<StandardAndResultDragAndDropModel>(task.Standard)?.StandardOrResult;
            
            if(task.Type == TaskTypeEnum.RequirementsTypeAndOrderByImportance)
            {
                SetGradeCalculator(new RequirementsTypeAndOrderByImportanceTaskCalculator());
            }

            else if(task.Type == TaskTypeEnum.SystemsTypeAndFindMostImportant)
            {
                SetGradeCalculator(new SystemsTypeAndFindMostImportantCalculator());
            }

            var taskResult = _gradeCalculator.CalculateTaskResult(standard, input, userId, task);
            await _taskResultRepository.CreateAsync(taskResult);

            return new StudentTaskResultOutputModel()
            {
                Id = taskResult.Id,
                ErrorCount = taskResult.ErrorCount,
                FinalMark = taskResult.FinalMark,
                Percentage = taskResult.Percentage,
                Result = taskResult.Result
            };
        }
    }
}
