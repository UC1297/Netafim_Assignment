namespace Netafim_Assignment.Models
{
    public class TimerInfo
    {

        public string TimerId { get; init; }
        public DateTime EndTime { get; init; }
        public string CallbackUrl { get; init; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; init; }


    }
}
