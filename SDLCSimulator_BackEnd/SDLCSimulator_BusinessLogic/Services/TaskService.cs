using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SDLCSimulator_BusinessLogic.Helpers;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;
using SDLCSimulator_Data.Enums;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class TaskService : ITaskService

    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<StudentTasksOutputModel>> GetFilteredTasksWithTaskResultsForStudentAsync(
            TaskForStudentFilterInput filterInput, int groupId, int userId)
        {
            var tasks = await _taskRepository.GetTasksWithTaskResultsForStudent(groupId, userId).ToListAsync();
            if (filterInput != null)
            {
                if (filterInput.Difficulties != null)
                {
                    tasks = tasks.Where(t => filterInput.Difficulties.Any(d => d == t.Difficulty)).ToList();
                }

                if (filterInput.Types != null)
                {
                    tasks = tasks.Where(t => filterInput.Types.Any(d => d == t.Type)).ToList();
                }

                if (!string.IsNullOrEmpty(filterInput.Topic))
                {
                    tasks = tasks.Where(t => t.Topic.StartsWith(filterInput.Topic)).ToList();
                }
            }

            var result = tasks.Select(t => new StudentTasksOutputModel()
            {
                Id = t.Id,
                Difficulty = t.Difficulty,
                Type = t.Type,
                Topic = t.Topic,
                Description = t.Description,
                Standard = t.Standard,
                ErrorRate = ErrorRateGetter.GetErrorRate(t.ErrorRate),
                MaxGrade = (int)t.MaxGrade,
                TeacherFirstName = t.Teacher.FirstName,
                TeacherLastName = t.Teacher.LastName,
                StudentsTaskResults = t.TaskResults.Select(tr => new StudentTaskResultOutputModel()
                {
                    Id = tr.Id,
                    ErrorCount = tr.ErrorCount,
                    Percentage = tr.Percentage,
                    FinalMark = tr.FinalMark,
                    Result = tr.Result
                }).ToList()
            }).ToList();

            return result;
        }

        public async Task<List<TeacherTasksOutputModel>> GetFilteredTasksWithTaskResultsForTeacherAsync(
            TaskForTeacherFilterInput filterInput, int userId)
        {
            var tasks = await _taskRepository.GetTasksWithTaskResultsForTeacher(userId).ToListAsync();
            if (filterInput != null)
            {
                if (!string.IsNullOrEmpty(filterInput.FirstName))
                {
                    tasks = tasks.Where(t => t.TaskResults.Any(tr => tr.Student.FirstName.StartsWith(filterInput.FirstName))).ToList();
                    tasks.ForEach(t =>
                    {
                        t.TaskResults = t.TaskResults
                            .Where(tr => tr.Student.FirstName.StartsWith(filterInput.FirstName)).ToList();
                    });
                }

                if (!string.IsNullOrEmpty(filterInput.LastName))
                {
                    tasks = tasks.Where(t => t.TaskResults.Any(tr => tr.Student.LastName.StartsWith(filterInput.LastName))).ToList();
                    tasks.ForEach(t =>
                    {
                        t.TaskResults = t.TaskResults
                            .Where(tr => tr.Student.LastName.StartsWith(filterInput.LastName)).ToList();
                    });
                }
                if (!string.IsNullOrEmpty(filterInput.GroupName))
                {
                    tasks = tasks.Where(t => t.GroupTasks.Any(tr => tr.Group.GroupName == filterInput.GroupName)).ToList();
                    tasks.ForEach(t =>
                    {
                        t.TaskResults = t.TaskResults
                            .Where(tr => tr.Student.Group.GroupName == filterInput.GroupName).ToList();
                    });
                }

                if (!string.IsNullOrEmpty(filterInput.Topic))
                {
                    tasks = tasks.Where(t => t.Topic.StartsWith(filterInput.Topic)).ToList();
                }
            }

            var result = tasks.Select(t => new TeacherTasksOutputModel()
            {
                Id = t.Id,
                Difficulty = t.Difficulty,
                Type = t.Type,
                Topic = t.Topic,
                Description = t.Description,
                Standard = t.Standard,
                ErrorRate = ErrorRateGetter.GetErrorRate(t.ErrorRate),
                MaxGrade = (int) t.MaxGrade,
                Groups = t.GroupTasks.Select(gt => new GroupModel()
                {
                    Id = gt.Group.Id,
                    GroupName = gt.Group.GroupName
                }).ToList(),
                TeachersTaskResults = t.TaskResults.Select(tr => new TeacherTaskResultOutputModel()
                {
                    Id = tr.Id,
                    ErrorCount = tr.ErrorCount,
                    Percentage = tr.Percentage,
                    FinalMark = tr.FinalMark,
                    Result = tr.Result,
                    StudentFirstName = tr.Student.FirstName,
                    StudentLastName = tr.Student.LastName,
                    GroupName = tr.Student.Group.GroupName
                }).ToList()
            }).ToList();

            return result;
        }

    }
}
