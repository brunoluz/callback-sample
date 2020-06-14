using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace callback_sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LongRunningTaskController : ControllerBase
    {
        const string TIME_FORMAT = "dd/MM/yyyy HH:mm:ss";

        private readonly ILogger<LongRunningTaskController> _logger;

        public LongRunningTaskController(ILogger<LongRunningTaskController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return StatusCode(200, "alive");
        }

        [HttpPost("{id}")]
        public ActionResult Post()
        {
            _logger.LogInformation("Request started at {0}.", DateTime.Now.ToString(TIME_FORMAT));

            Task.Run(() => {
                Thread.Sleep(5000);
                _logger.LogInformation("Task ended at {0}.", DateTime.Now.ToString(TIME_FORMAT));
            });

            _logger.LogInformation("Request finished at {0}.", DateTime.Now.ToString(TIME_FORMAT));

            return StatusCode(200, "deu certo");
        }
    }
}
