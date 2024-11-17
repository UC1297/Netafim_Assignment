namespace Netafim_Assignment.Services
{
    public class TimerWorker : BackgroundService
    {
        private readonly ITimerService _timerService;
        private readonly IHttpClientFactory _httpClientFactory;

        public TimerWorker(ITimerService timerService, IHttpClientFactory httpClientFactory)
        {
            _timerService = timerService;
            _httpClientFactory = httpClientFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var expiredTimers = _timerService.FindExpiredTimers();

                foreach (var timer in expiredTimers)
                {
                    await TriggerWebhook(timer.CallbackUrl, timer.TimerId);
                    timer.Status = "Expired";
                }

                _timerService.SaveTimersToFile();
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task TriggerWebhook(string callbackUrl, string timerId)
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();
                var obj = new
                {
                    timerId = timerId,
                    callbackUrl = callbackUrl,
                    
                };

                JsonContent content = JsonContent.Create(obj);
                await client.PostAsync(callbackUrl, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while calling webhook: {ex.Message}");
            }
        }
    }
}
