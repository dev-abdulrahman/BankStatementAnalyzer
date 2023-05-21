using BankStatementAnalyzer.BusinessLogic.Common;
using BankStatementAnalyzer.Models;
using System.Threading.Tasks;

namespace BankStatementAnalyzer.BusinessLogic.Interface
{
    public interface IPushNotificationService : IGenericService<PushNotification>
    {
        Task<bool> Send(string title, string text, string notificationKeyOrDeviceId, string serverValue, string url);
    }
}