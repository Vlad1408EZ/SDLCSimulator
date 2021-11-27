using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.General;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;
using SDLCSimulator_Data;
using SDLCSimulator_Data.Enums;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IEmailService _emailService;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IAuthService authService, IGroupRepository groupRepository,
            IEmailService emailService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _groupRepository = groupRepository;
            _emailService = emailService;
        }

        public async Task<AuthenticateResponseModel> LoginAsync(AuthenticateRequestModel model)
        {
            var user = await _userRepository.GetSingleByConditionAsync(u => u.Email == model.Email);

            if (user == null || user.IsDeleted || !_authService.VerifyPassword(user.Password, model.Password))
            {
                throw new InvalidOperationException($"Користувач не знайдений або введено неправильний пароль");
            }

            var token = _authService.GenerateWebTokenForUser(user);

            return new AuthenticateResponseModel(user, token);  
        }

        public async Task<UpdateUserInfoModel> UpdateUserInfoAsync(UpdateUserInfoModel model, int userId)
        {
            var user = await _userRepository.GetSingleByConditionAsync(u => u.Id == userId);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            await _userRepository.UpdateAsync(user);

            return new UpdateUserInfoModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordRequestModel model, int userId)
        {
            var user = await _userRepository.GetSingleByConditionAsync(u => u.Id == userId);
            if (user == null || !_authService.VerifyPassword(user.Password, model.OldPassword))
            {
                throw new InvalidOperationException($"Користувач не знайдений або введено неправильний пароль");
            }

            user.Password = _authService.HashPassword(model.NewPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<List<UserOutputModel>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAll().Include(u => u.Group)
                .Include(u => u.GroupTeachers).ThenInclude(gt => gt.Group).ToListAsync();

            var result = users.Select(u => new UserOutputModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Role = u.Role,
                IsDeleted = u.IsDeleted,
                Email = u.Email,
                Groups = u.Group != null ? new List<string> {u.Group.GroupName}
                    : u.GroupTeachers.Any() ? u.GroupTeachers.Select(gt => gt.Group.GroupName).ToList()
                    : new List<string>()
            }).ToList();

            return result;
        }

        public async Task<UserOutputModel> CreateUserAsync(CreateUserInputModel model)
        {
            var user = await _userRepository.GetSingleByConditionAsync(u => u.Email == model.Email);
            if (user != null)
            {
                throw new InvalidOperationException($"Користувач з імейлом '{model.Email}' вже існує");
            }

            var groups = await _groupRepository.GetByCondition(g => model.Groups.Any(gr => g.GroupName == gr))
                .ToListAsync();

            if (groups.Count != model.Groups.Count)
            {
                throw new InvalidOperationException($"Вхідні дані про групи не валідні");
            }

            user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = _authService.HashPassword(model.Password),
                Role = model.Role
            };
            if (user.Role == RoleEnum.Student)
            {
                user.GroupId = groups[0].Id;
            }

            else if (user.Role == RoleEnum.Teacher)
            {
                user.GroupTeachers = groups.Select(g => new GroupTeacher
                {
                    GroupId = g.Id
                }).ToList();
            }

            await _userRepository.CreateAsync(user);

            await _emailService.ListenToEmailSendingMessagesForUserAsync(new EmailUserModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password
            });

            return new UserOutputModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Email = user.Email,
                Groups = groups.Select(g => g.GroupName).ToList()
            };
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetSingleByConditionAsync(u => u.Id == userId);
            if (user != null)
            {
                throw new InvalidOperationException($"Користувач з айді '{userId}' вже існує");
            }

            user.IsDeleted = true;
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
