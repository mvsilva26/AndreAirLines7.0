    using Models.ModelSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngestaoAirport.Repository
{
    public interface IAirportRepository
    {

        bool Add(Airport airport);
        List<Airport> GetAll();




        
    }
}
