using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelSQL
{


    [Table("Airport")]
    public class Airport
    {

        #region Constant

        public readonly static string INSERT = "INSERT INTO Airport (City, Country, Code, Continent) VALUES (@City, @Country, @Code, @Continent)";
        public readonly static string GETALL = "SELECT Id City, Country, Code, Continent FROM Airport";
        public readonly static string DELETE = "DELETE FROM Airport WHERE Id = ";
        public readonly static string GET = "SELECT Id City, Country, Code, Continent FROM Airport WHERE ID = ";
        public readonly static string UPDATE = "UPDATE Airport SET City = @City,Country = @Country,Code = @Code,Continent = @Continent WHERE Id = @Id";

        #endregion


        #region Properties
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Code { get; set; }
        public string Continent { get; set; }


      

        public Airport()
        {


        }
        public Airport(string city, string country, string code, string continent)
        {
            City=city;
            Country=country;
            Code=code;
            Continent=continent;
        }

        #endregion



        #region Method
        public override string ToString()
        {
            return "Id: " + Id +
                   "\nCity: " + City +
                   "\nCountry: " + Country +
                   "\nCode: " + Code +
                   "\nContinent: " + Continent;
        }
        #endregion




    }
}
