using Dapper;
using Models.ModelSQL;
using ProjAirportDaper.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ProjAirportDaper.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private string _con;
        public AirportRepository()
        {
            _con = DataBaseConfiguration.Get();
        }
        public bool Add(Airport airport)
        {
            bool status = false;

            using (var db = new SqlConnection(_con))
            {
                db.Open();
                db.Execute(Airport.INSERT, airport);
                status = true;
            }

            return status;
        }

        public Airport Get(string id)
        {
            using (var db = new SqlConnection(_con))
            {
                db.Open();
                var airport = db.QueryFirstOrDefault<Airport>(Airport.GET + id);
                return (Airport)airport;
            }
        }

        public List<Airport> GetAll()
        {
            using (var db = new SqlConnection(_con))
            {
                db.Open();
                var airport = db.Query<Airport>(Airport.GETALL);
                return (List<Airport>)airport;
            }
        }

        public void Remove(string id)
        {
            using (var db = new SqlConnection(_con))
            {
                db.Open();

                db.Execute(Airport.DELETE + id);
            }
        }

        public void Update(Airport airport)
        {
            using (var db = new SqlConnection(_con))
            {
                db.Open();
                db.Execute(Airport.UPDATE, airport);
            }
        }


    }
}
