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
                return null;
            }

            var token = _authService.GenerateWebTokenForUser(user);

            return new AuthenticateResponseModel(user, token);  
        }
    }
}
