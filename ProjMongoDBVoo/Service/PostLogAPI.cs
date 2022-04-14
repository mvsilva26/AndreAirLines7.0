using Models.Model;
using System.Net.Http;
using System.Net.Http.Json;

namespace ProjMongoDBVoo.Service
{
    public class PostLogAPI
    {
        HttpClient ApiConnection = new HttpClient();
        public static void PostLog(Log log)
        {
            HttpClient ApiConnection = new HttpClient();

            ApiConnection.PostAsJsonAsync("https://localhost:44360/api/Log", log);

        }
    }

}

