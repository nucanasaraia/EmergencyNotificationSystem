using EmergencyNotifRespons.Models;

namespace EmergencyNotifRespons.Services.Interfaces
{
    public interface IUserLoggerService
    {
        void LogInfo(User? user, string message, params object[] args);
        void LogWarning(User? user, string message, params object[] args);
        void LogError(User? user, Exception? ex, string message, params object[] args);
    }
}
