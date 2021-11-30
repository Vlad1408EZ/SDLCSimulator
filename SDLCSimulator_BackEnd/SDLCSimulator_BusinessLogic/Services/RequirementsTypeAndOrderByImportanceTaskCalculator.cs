using Newtonsoft.Json;
using SDLCSimulator_BusinessLogic.Helpers;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskModel = SDLCSimulator_Data.Task;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class RequirementsTypeAndOrderByImportanceTaskCalculator : IGradeCalculator
    {
        public TaskResult CalculateTaskResult(Dictionary<string, List<string>> standard, CreateTaskResultInput input, int userId, TaskModel task)
        {
            var result = input.Result;
            var correctAnswersNumber = 0;
            var questionsNumber = standard.Values.Sum(w => w.Count);
            foreach (var res in result.StandardOrResult)
            {
                var resValue = res.Value;
                var standardValue = standard[res.Key];
                correctAnswersNumber -= standardValue.Count - resValue.Count;
                for (int i = 0; i < resValue.Count; i++)
                {
                    if (resValue[i] == standardValue[i])
                        correctAnswersNumber++;
                }
            }

            var percentage = correctAnswersNumber / (decimal)questionsNumber;
            var finalMark = (int)task.MaxGrade * percentage - input.ErrorCount * ErrorRateGetter.GetErrorRate(task.ErrorRate);
            var taskResult = new TaskResult()
            {
                StudentId = userId,
                TaskId = task.Id,
                Percentage = percentage,
                ErrorCount = input.ErrorCount,
                FinalMark = finalMark < 0 ? 0 : finalMark,
                Result = JsonConvert.SerializeObject(input.Result),
            };

            return taskResult;
        }
    }
}
