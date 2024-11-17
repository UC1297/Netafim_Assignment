namespace Netafim_Assignment.Services
{
    using Netafim_Assignment.Models;
    using System;
    using System.Collections.Concurrent;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    public class TimerService : ITimerService
    {
        private readonly Dictionary<string, TimerInfo> _activeTimers = new();
        private const string PersistenceFilePath = "timers_data.json";

        public TimerService()
        {
            ReloadTimersFromFile();
        }

        public string CreateNewTimer(TimerRequest request)
        {
            string timerId = Guid.NewGuid().ToString();
            DateTime endTime = DateTime.UtcNow.AddHours(request.Hours)
                                              .AddMinutes(request.Minutes)
                                              .AddSeconds(request.Seconds);

            var newTimer = new TimerInfo
            {
                TimerId = timerId,
                EndTime = endTime,
                CallbackUrl = request.WebhookUrl,
                Status = "Running",
                CreatedAt = DateTime.UtcNow
            };

            _activeTimers[timerId] = newTimer;
            SaveTimersToFile();
            return timerId;
        }

        public TimerStatusResponse GetTimerStatusById(string timerId)
        {
            if (_activeTimers.TryGetValue(timerId, out var timer))
            {
                int remainingTime = (int)(timer.EndTime - DateTime.UtcNow).TotalSeconds;
                if (remainingTime <= 0)
                {
                    timer.Status = "Expired";
                    remainingTime = 0;
                }

                return new TimerStatusResponse
                {
                    TimerId = timer.TimerId,
                    RemainingSeconds = remainingTime,
                    CurrentStatus = timer.Status
                };
            }
            return null;
        }

        public List<TimerInfo> FindExpiredTimers()
        {
            return _activeTimers.Values.Where(t => t.Status == "Running" && t.EndTime <= DateTime.UtcNow).ToList();
        }

        public void SaveTimersToFile()
        {
            var serializedData = JsonSerializer.Serialize(_activeTimers);
            File.WriteAllText(PersistenceFilePath, serializedData);
        }

        public void ReloadTimersFromFile()
        {
            if (File.Exists(PersistenceFilePath))
            {
                var fileData = File.ReadAllText(PersistenceFilePath);
                var timers = JsonSerializer.Deserialize<Dictionary<string, TimerInfo>>(fileData);
                if (timers != null)
                {
                    foreach (var timer in timers)
                    {
                        _activeTimers[timer.Key] = timer.Value;
                    }
                }
            }
        }


    }
}