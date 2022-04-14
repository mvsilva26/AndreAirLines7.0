using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ModelSQL;
using ProjAirportDaper.Service;
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
        public ActionResult<Airport> Create(Airport airport)
        {

            _airportService.Add(airport);
            return (airport);

        }

        //[HttpDelete]
        //public IActionResult Delete(string id)
        //{
        //    var airport = _airportService.Get(id);
        //    if(airport == null)
        //    {
        //        return NotFound();
        //    }

        //    _airportService.Remove(id);
        //    return NoContent();
        //}


        //[HttpPut]
        //public IActionResult Update(string id, Airport airportIn)
        //{
        //    var airport = _airportService.Get(id);
        //    airportIn.Id = airport.Id;
        //    if (airport == null)
        //    {
        //        return NotFound();
        //    }
        //    _airportService.UpDate(airportIn);
        //    return NoContent();
        //}


        //[HttpGet("Search")]
        //public ActionResult<Airport> Get(string id) =>
        //    _airportService.Get(id);



    }
}
