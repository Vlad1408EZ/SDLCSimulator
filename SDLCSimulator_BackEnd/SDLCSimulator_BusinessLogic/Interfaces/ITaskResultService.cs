using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Models.Input;
using SDLCSimulator_BusinessLogic.Models.Output;

namespace SDLCSimulator_BusinessLogic.Interfaces
{
    public interface ITaskResultService
    {
        Task<StudentTaskResultOutputModel> SetTaskResultAsync(CreateTaskResultInput input,int userId);
    }
}
