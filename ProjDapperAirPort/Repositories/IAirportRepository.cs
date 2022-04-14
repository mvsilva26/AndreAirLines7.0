using Models.ModelSQL;
using System.Collections.Generic;

namespace ProjAirportDaper.Repositories
{
    public interface IAirportRepository
    {

        bool Add(Airport airport);
        List<Airport> GetAll();
        void Remove(string id);
        Airport Get(string id);
        void Update(Airport airport);


    }
}
