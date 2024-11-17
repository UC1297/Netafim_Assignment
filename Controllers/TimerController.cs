using Microsoft.AspNetCore.Mvc;
using Netafim_Assignment.Models;
using Netafim_Assignment.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Netafim_Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimerController : ControllerBase
    {

        private readonly ITimerService _timerService;

        public TimerController(ITimerService timerService)
        {
            _timerService = timerService;
        }

        [HttpPost("create")]
        public IActionResult CreateTimer([FromBody] TimerRequest request)
        {
            var timerId = _timerService.CreateNewTimer(request);
            return Ok(new { TimerId = timerId });
        }

        [HttpGet("status/{timerId}")]
        public IActionResult GetTimerStatus(string timerId)
        {
            var status = _timerService.GetTimerStatusById(timerId);
            if (status == null)
            {
                return NotFound(new { Message = "Timer not found" });
            }
            return Ok(status);
        }

    }
}
