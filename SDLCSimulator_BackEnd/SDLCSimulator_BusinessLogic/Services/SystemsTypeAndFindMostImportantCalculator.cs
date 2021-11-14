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

namespace SDLCSimulator_BusinessLogic.Services
{
    public class SystemsTypeAndFindMostImportantCalculator : IGradeCalculator
    {
        private const int DecreaseErrorRate = 5;
        public TaskResult CalculateTaskResult(Dictionary<string, List<string>> standard, CreateTaskResultInput input, int userId, SDLCSimulator_Data.Task task)
        {
            var result = input.Result;
            var correctAnswersNumber = 0;
            var valuesNumber = standard.Values.Sum(w => w.Count);
            foreach (var res in result.StandardOrResult)
            {
                var resValue = res.Value;
                var standardValue = standard[res.Key];
                foreach(var elem in resValue)
                {
                    if(standardValue.Contains(elem))
                    {
                        correctAnswersNumber++;
                    }
                }
            }

            var percentage = correctAnswersNumber / (decimal)valuesNumber;
            var taskResult = new TaskResult()
            {
                StudentId = userId,
                TaskId = task.Id,
                Percentage = percentage,
                ErrorCount = input.ErrorCount,
                FinalMark = (int)task.MaxGrade * percentage - input.ErrorCount * ErrorRateGetter.GetErrorRate(task.ErrorRate) / DecreaseErrorRate,
                Result = JsonConvert.SerializeObject(input.Result),
            };

            return taskResult;
        }
    }
}
