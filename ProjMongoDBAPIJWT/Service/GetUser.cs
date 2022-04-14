using Models.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ProjMongoDBAPIJWT.Service
{
    public class GetUser
    {

        public static async Task<User> GetLogin(string login, string password)
        {

            HttpClient ApiConnection = new HttpClient();

            User user = new User() {Login = login, Senha = password };

            HttpResponseMessage userOUT = await ApiConnection.PostAsJsonAsync("https://localhost:44310/api/User/GetLogin", user);

            ApiConnection.DefaultRequestHeaders.Accept.Clear();
            ApiConnection.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string responseBody = await userOUT.Content.ReadAsStringAsync();
            var userLogin = JsonConvert.DeserializeObject<User>(responseBody);
            if (userLogin == null)
            {
                return null;
            }
            else
            {
                return userLogin;

            }

        }

    }
}
