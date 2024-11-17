namespace Netafim_Assignment.Models
{
    public class TimerStatusResponse
    {
        public string TimerId { get; set; }
        public int RemainingSeconds { get; set; }
        public string CurrentStatus { get; set; }

    }
}
