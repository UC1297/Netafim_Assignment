using Netafim_Assignment.Models;
using static Netafim_Assignment.Services.TimerService;

namespace Netafim_Assignment.Services
{
    public interface ITimerService
    {
        string CreateNewTimer(TimerRequest request);
        TimerStatusResponse GetTimerStatusById(string timerId);
        List<TimerInfo> FindExpiredTimers();
        void SaveTimersToFile();
        void ReloadTimersFromFile();
    }
}
