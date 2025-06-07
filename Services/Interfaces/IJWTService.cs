using EmergencyNotifRespons.CORE;
using EmergencyNotifRespons.Models;

namespace EmergencyNotifRespons.Services.Interfaces
{
    public interface IJWTService
    {
        UserToken GetUserToken(User user);
        int GetCurrentUserId();
    }
}
