using Dapper;
using IngestaoAirport.Config;
using Models.ModelSQL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IngestaoAirport.Repository
{
    public class AirportRepository : IAirportRepository
    {

        private string _conn;

        public AirportRepository()
        {

            _conn = DataBaseConfiguration.Get();

        }

        public bool Add(Airport airport)
        {
            bool status = false;

            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                db.Execute(Airport.INSERT, airport);
                status = true;
            }


            return status;
        }


        public List<Airport> GetAll()
        {
            using (var db = new SqlConnection(_conn))
            {
                db.Open();
                var airport = db.Query<Airport>(Airport.GETALL);
                return (List<Airport>)airport;
            }
        }

       

    }
}
