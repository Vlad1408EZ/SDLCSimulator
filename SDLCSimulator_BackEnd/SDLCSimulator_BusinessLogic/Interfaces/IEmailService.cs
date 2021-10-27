using System.Threading.Tasks;
using SDLCSimulator_BusinessLogic.Models.General;

namespace SDLCSimulator_BusinessLogic.Interfaces
{
    public interface IEmailService
    {
        Task ListenToEmailSendingMessagesForUserAsync(EmailUserModel model);
        Task ListenToEmailSendingMessagesForTaskAsync(EmailTaskModel model);
    }
}
