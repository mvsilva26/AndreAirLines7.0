using IngestaoAirport.Repository;
using Models.ModelSQL;
using System.Collections.Generic;

namespace ProjAirportDaper.Service
{
    public class AirportService
    {

        private IAirportRepository _airportRepository;


        public AirportService()
        {
            _airportRepository = new AirportRepository();
        }

        public bool Add(Airport airport)
        {
            return _airportRepository.Add(airport);
        }

        public List<Airport> GetAll()
        {
            return _airportRepository.GetAll();
        }

        //public Airport Get(string id)
        //{
        //    return _airportRepository.Get(id);
        //}

        //public void Remove(string id)
        //{
        //    _airportRepository.Remove(id);
        //}

        //public void UpDate(Airport airport)
        //{
        //    _airportRepository.Update(airport);
        //}






    }
}
