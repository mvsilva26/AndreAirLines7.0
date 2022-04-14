using APILog.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;

namespace APILog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {

        private readonly LogService _logService;


        public LogController(LogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public ActionResult<Log> Create(Log log)
        {

            _logService.Create(log);

            return CreatedAtRoute("GetLog", new { id = log.Id.ToString() }, log);

        }


    }
}
