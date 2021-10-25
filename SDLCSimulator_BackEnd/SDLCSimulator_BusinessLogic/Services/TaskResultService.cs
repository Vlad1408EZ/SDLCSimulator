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
using SDLCSimulator_Data.JsonTaskModels;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class TaskResultService : ITaskResultService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskResultRepository _taskResultRepository;

        public TaskResultService(ITaskRepository taskRepository, ITaskResultRepository taskResultRepository)
        {
            _taskRepository = taskRepository;
            _taskResultRepository = taskResultRepository;
        }

        public async Task<StudentTaskResultOutputModel> SetTaskResultAsync(TaskResultInput input, int userId)
        {
            var task = await _taskRepository.GetSingleByConditionAsync(t => t.Id == input.TaskId);
            if (task == null)
            {
                throw new InvalidOperationException($"Task with id {input.TaskId} is not found");
            }
            var result = input.Result;
            var standard = JsonConvert.DeserializeObject<StandardAndResultDragAndDropModel>(task.Standard)?.StandardOrResult;
            var correctAnswersNumber = 0;
            var questionsNumber = standard.Values.Sum(w => w.Count);
            foreach (var res in result.StandardOrResult)
            {
                var resValue = res.Value;
                var standardValue = standard[res.Key];
                for (int i = 0; i < resValue.Count; i++)
                {
                    if (resValue[i] == standardValue[i])
                        correctAnswersNumber++;
                }
            }

            var percentage = correctAnswersNumber / (decimal)questionsNumber;
            var taskResult = new TaskResult()
            {
                StudentId = userId,
                TaskId = task.Id,
                Percentage = percentage,
                ErrorCount = input.ErrorCount,
                FinalMark = (int)task.MaxGrade * percentage - input.ErrorCount * ErrorRateGetter.GetErrorRate(task.ErrorRate),
                Result = JsonConvert.SerializeObject(input.Result),
            };

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
