using Models.ModelSQL;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestAPIDapperAndEntity
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DateTime startDapper, finishDapper, startedEntity, finishEntity;
            Console.WriteLine("Performance test");
            Console.WriteLine("Dapper vs Entity");
            Console.WriteLine("First test with Dapper");
            Console.WriteLine("Press a key to start");
            Console.ReadKey();
            Console.WriteLine("Started in {0}", startDapper=DateTime.Now);
            for (int i = 1; i <= 500; i++)
            {
                TestApiDapper(i).Wait();
            }
            Console.WriteLine("Finished in {0}", finishDapper =DateTime.Now);
            var resultDapper = startDapper - finishDapper;
            Console.WriteLine("Performance Dapper, Started in: {0} Finished in: {1} Result: {2}", startDapper, finishDapper, resultDapper);


            Console.WriteLine("Now test with Entity");
            Console.WriteLine("Press a key to start");
            Console.ReadKey();
            Console.WriteLine("Started in {0}", startedEntity = DateTime.Now);
            for (int i = 1; i <= 500; i++)
            {
                TestApiEntity(i).Wait();
            }
            Console.WriteLine("Finished in {0}", finishEntity = DateTime.Now);
            var resultEntity = startedEntity - finishEntity;
            Console.WriteLine("Performance Entity, Started in: {0} Finished in: {1} Result: {2}", startedEntity, finishEntity, resultEntity);
            if (resultEntity < resultDapper)
            {
                Console.WriteLine("Winner Dapper");
                Console.WriteLine("Result: Dapper: {0} Entity: {1}", resultDapper, resultEntity);
            }
            else
            {
                Console.WriteLine("Winner Entity");
                Console.WriteLine("Result: Entity: {0} Dapper: {1}", resultEntity, resultDapper);

            }


        }
        public static async Task<Airport> TestApiDapper(int id)
        {
            HttpClient ApiConnection = new HttpClient();
            Airport airport = new Airport();


            HttpResponseMessage airportData = await ApiConnection.GetAsync("https://localhost:44347/api/Airport/Search?id="+id);
            string responseBody = await airportData.Content.ReadAsStringAsync();
            airport = JsonConvert.DeserializeObject<Airport>(responseBody);
            Console.WriteLine("Code: {0}", id);
            return airport;
        }
        public static async Task<Airport> TestApiEntity(int id)
        {
            HttpClient ApiConnection = new HttpClient();
            Airport airport = new Airport();


            HttpResponseMessage airportData = await ApiConnection.GetAsync("https://localhost:44311/api/Airports/" + id);
            string responseBody = await airportData.Content.ReadAsStringAsync();
            airport = JsonConvert.DeserializeObject<Airport>(responseBody);
            Console.WriteLine("Code: {0}", id);
            return airport;
        }



    }

}
