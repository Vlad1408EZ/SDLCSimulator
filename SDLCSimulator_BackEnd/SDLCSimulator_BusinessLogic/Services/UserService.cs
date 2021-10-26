using System;
using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Interfaces;
using SDLCSimulator_BusinessLogic.Models.General;
using SDLCSimulator_Repository.Interfaces;

namespace SDLCSimulator_BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<AuthenticateResponseModel> LoginAsync(AuthenticateRequestModel model)
        {
            var user = await _userRepository.GetSingleByConditionAsync(u => u.Email == model.Email);

            if (user == null || !_authService.VerifyPassword(user.Password, model.Password))
            {
                throw new InvalidOperationException($"User is not found or password is incorrect");
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
                throw new InvalidOperationException($"User is not found or password is incorrect");
            }

            user.Password = _authService.HashPassword(model.NewPassword);
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
