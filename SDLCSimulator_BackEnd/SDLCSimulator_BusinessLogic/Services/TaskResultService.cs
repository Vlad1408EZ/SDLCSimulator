using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        private readonly IDbContextFactory<SDLCSimulatorDbContext> _contextFactory;
        private IGradeCalculator _gradeCalculator;

        public TaskResultService(IDbContextFactory<SDLCSimulatorDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        private void SetGradeCalculator(IGradeCalculator gradeCalculator)
        {
            _gradeCalculator = gradeCalculator;
        }

        internal Type GetCalculatorType()
        {
            return _gradeCalculator.GetType();
        }

        public async Task<StudentTaskResultOutputModel> SetTaskResultAsync(CreateTaskResultInput input, int userId)
        {
            var context = _contextFactory.CreateDbContext();

            var task = await context.Tasks.FirstOrDefaultAsync(t => t.Id == input.TaskId);
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
                SetGradeCalculator(new SystemsTypeAndFindMostImportantTaskCalculator());
            }

            var taskResult = _gradeCalculator.CalculateTaskResult(standard, input, userId, task);
            context.TaskResults.Add(taskResult);

            await context.SaveChangesAsync();

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
