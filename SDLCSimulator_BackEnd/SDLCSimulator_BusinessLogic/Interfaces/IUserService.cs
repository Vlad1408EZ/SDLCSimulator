using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Models.General;

namespace SDLCSimulator_BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<AuthenticateResponseModel> LoginAsync(AuthenticateRequestModel model);
        Task<UpdateUserInfoModel> UpdateUserInfoAsync(UpdateUserInfoModel model,int userId);
        Task<bool> ChangePasswordAsync(ChangePasswordRequestModel model,int userId);
    }
}
