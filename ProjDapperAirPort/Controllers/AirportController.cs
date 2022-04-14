using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ModelSQL;
using ProjDapperAirPort.Service;
using System.Collections.Generic;

namespace ProjAirportDaper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {

        private readonly AirportService _airportService = new AirportService();

        public AirportController(AirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        public ActionResult<List<Airport>> Get() =>
            _airportService.GetAll();

        [HttpPost]
        public ActionResult<Airport> Create(Airport aiport)
        {
            _airportService.Add(aiport);
            return (aiport);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var airport = _airportService.Get(id);
            if (airport == null)
            {
                return NotFound();
            }
            _airportService.Remove(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult Update(string id, Airport airportIn)
        {
            var aiport = _airportService.Get(id);
            airportIn.Id = aiport.Id;
            if (aiport == null)
            {
                return NotFound();
            }
            _airportService.UpDate(airportIn);
            return NoContent();
        }



        [HttpGet("Search")]
        public ActionResult<Airport> Get(string id) =>
            _airportService.Get(id);




    }
}
