using System.Collections.Generic;
using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Models.General;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;

namespace SDLCSimulator_BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponseModel> LoginAsync(AuthenticateRequestModel model);
        Task<UpdateUserInfoModel> UpdateUserInfoAsync(UpdateUserInfoModel model,int userId);
        Task<bool> ChangePasswordAsync(ChangePasswordRequestModel model,int userId);
        Task<List<UserOutputModel>> GetAllUsersAsync();
        Task<UserOutputModel> CreateUserAsync(CreateUserInputModel model);
        Task<bool> DeleteUserAsync(int userId);
    }
}
