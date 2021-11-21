using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SDLCSimulator_BusinessLogic.Helpers;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.General;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;
using SDLCSimulator_Data;
using SDLCSimulator_Data.Enums;
using SDLCSimulator_Repository.Interfaces;
using TaskModel = SDLCSimulator_Data.Task;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IGroupTaskRepository _groupTaskRepository;
        private readonly ITaskResultRepository _taskResultRepository;
        private readonly IEmailService _queueEmailService;

        public TaskService(ITaskRepository taskRepository, IGroupRepository groupRepository, IGroupTaskRepository groupTaskRepository,
            ITaskResultRepository taskResultRepository, IEmailService queueEmailService)
        {
            _taskRepository = taskRepository;
            _groupRepository = groupRepository;
            _groupTaskRepository = groupTaskRepository;
            _taskResultRepository = taskResultRepository;
            _queueEmailService = queueEmailService;
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
                if (filterInput.GroupNames != null)
                {
                    tasks = tasks.Where(t => t.GroupTasks.Any(tr => filterInput.GroupNames.Contains(tr.Group.GroupName))).ToList();
                    tasks.ForEach(t =>
                    {
                        t.TaskResults = t.TaskResults
                            .Where(tr => filterInput.GroupNames.Contains(tr.Student.Group.GroupName)).ToList();
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
                MaxGrade = (int)t.MaxGrade,
                Groups = t.GroupTasks.Select(gt => new GroupOutputModel()
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

        public async Task<TeacherTasksOutputModel> CreateTaskAsync(CreateTaskInputModel model,int userId)
        {
            var groups = await _groupRepository.GetByCondition(g => model.GroupNames.Any(gn => g.GroupName == gn))
                .ToListAsync();

            if (groups.Count == 0)
            {
                throw new InvalidOperationException($"Вхідні дані про групу не валідні");
            }

            var task = new TaskModel
            {
                Type = model.Type,
                Difficulty = model.Difficulty,
                MaxGrade = GetMaxGrade(model.Difficulty),
                ErrorRate = GetErrorRate(model.Difficulty),
                Topic = model.Topic,
                Description = JsonConvert.SerializeObject(model.Description),
                Standard = JsonConvert.SerializeObject(model.Standard),
                TeacherId = userId
            };

            await _taskRepository.CreateAsync(task);
            var groupTasks = groups.Select(g => new GroupTask()
            {
                GroupId = g.Id,
                TaskId = task.Id
            }).ToList();

            await _groupTaskRepository.CreateRangeAsync(groupTasks);

            await _queueEmailService.ListenToEmailSendingMessagesForTaskAsync(new EmailTaskModel()
            {
                GroupIds = groups.Select(g => g.Id).ToList(),
                TeacherId = userId,
                Topic = task.Topic
            });

            return new TeacherTasksOutputModel()
            {
                Id = task.Id,
                Difficulty = task.Difficulty,
                Type = task.Type,
                Topic = task.Topic,
                Description = task.Description,
                Standard = task.Standard,
                ErrorRate = ErrorRateGetter.GetErrorRate(task.ErrorRate),
                MaxGrade = (int) task.MaxGrade,
                Groups = groups.Select(g => new GroupOutputModel()
                {
                    Id = g.Id,
                    GroupName = g.GroupName
                }).ToList(),
                TeachersTaskResults = new()
            };
        }

        public async Task<TeacherTasksOutputModel> UpdateTaskAsync(UpdateTaskInputModel model, int userId)
        {
            var task = await _taskRepository.GetSingleByConditionAsync(t => t.Id == model.TaskId && t.TeacherId == userId);
            if (task == null)
            {
                throw new InvalidOperationException($"Завдання з айді {model.TaskId} не знайдено");
            }

            var taskResults = _taskResultRepository.GetByCondition(tr => tr.TaskId == task.Id);

            if (await taskResults.AnyAsync())
            {
                throw new InvalidOperationException($"Завдання з айді {model.TaskId} не можна оновити оскільки має результати від студентів");
            }

            var groups = await _groupRepository.GetByCondition(g => model.GroupNames.Any(gn => g.GroupName == gn))
                .ToListAsync();

            if (groups.Count == 0)
            {
                throw new InvalidOperationException($"Вхідні дані про групу не валідні");
            }

            task.Type = model.Type;
            task.Difficulty = model.Difficulty;
            task.MaxGrade = GetMaxGrade(model.Difficulty);
            task.ErrorRate = GetErrorRate(model.Difficulty);
            task.Topic = model.Topic;
            task.Description = JsonConvert.SerializeObject(model.Description);
            task.Standard = JsonConvert.SerializeObject(model.Standard);
            await _groupTaskRepository.RemoveRangeAsync(
                await _groupTaskRepository.GetByCondition(gt => gt.TaskId == task.Id).ToListAsync());

            var groupTasks = groups.Select(g => new GroupTask()
            {
                GroupId = g.Id,
                TaskId = task.Id
            }).ToList();

            await _taskRepository.UpdateAsync(task);
            await _groupTaskRepository.CreateRangeAsync(groupTasks);

            return new TeacherTasksOutputModel()
            {
                Id = task.Id,
                Difficulty = task.Difficulty,
                Type = task.Type,
                Topic = task.Topic,
                Description = task.Description,
                Standard = task.Standard,
                ErrorRate = ErrorRateGetter.GetErrorRate(task.ErrorRate),
                MaxGrade = (int)task.MaxGrade,
                Groups = groups.Select(g => new GroupOutputModel()
                {
                    Id = g.Id,
                    GroupName = g.GroupName
                }).ToList(),
                TeachersTaskResults = new()
            };
        }

        public async Task<bool> RemoveTaskAsync(int userId, int taskId)
        {
            var task = await _taskRepository.GetSingleByConditionAsync(t => t.Id == taskId && t.TeacherId == userId);
            if (task == null)
            {
                throw new InvalidOperationException($"Завдання з айді {taskId} не знайдено");
            }

            var taskResults = _taskResultRepository.GetByCondition(tr => tr.TaskId == task.Id);

            if (await taskResults.AnyAsync())
            {
                throw new InvalidOperationException($"Завдання з айді {taskId} не можна видалити оскільки має результати від студентів");
            }

            await _taskRepository.RemoveAsync(task);
            return true;
        }

        private static ErrorRateEnum GetErrorRate(DifficultyEnum difficulty)
        {
            return difficulty switch
            {
                DifficultyEnum.Easy =>
                 ErrorRateEnum.EasyErrorRate,
                DifficultyEnum.Medium =>
                    ErrorRateEnum.MediumErrorRate,
                DifficultyEnum.Hard =>
                     ErrorRateEnum.HardErrorRate,
                _ => 0
            };
        }

        private static MaxGradeEnum GetMaxGrade(DifficultyEnum difficulty)
        {
            return difficulty switch
            {
                DifficultyEnum.Easy =>
                    MaxGradeEnum.EasyGrade,
                DifficultyEnum.Medium =>
                    MaxGradeEnum.MediumGrade,
                DifficultyEnum.Hard =>
                    MaxGradeEnum.HardGrade,
                _ => 0
            };
        }
    }
}
