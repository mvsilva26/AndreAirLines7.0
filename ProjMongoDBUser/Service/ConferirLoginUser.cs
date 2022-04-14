using Models.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjMongoDBUser.Service
{
    public class ConferirLoginUser
    {

        HttpClient ApiConnection = new HttpClient();
        public static async Task<BaseResponse> GetLogin(User userIn)
        {
            var baseResponse = new BaseResponse();
            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44314/api/User/GetLogin?loginUser=" + userIn.LoginUser);
            string responseBody = await user.Content.ReadAsStringAsync();
            var userLogin = JsonConvert.DeserializeObject<User>(responseBody);

            if (userLogin.Login == null)
            {
                baseResponse.Connecterro("Usuário não encontrado");
                return baseResponse;
            }
            else
            {
                if (userLogin.Function.Id == "1")
                {
                    baseResponse.ConnectResult(user);
                    return baseResponse;
                }
                else
                {
                    baseResponse.Connecterro("Usuário sem permissão para criar aeronaves");
                    return baseResponse;
                }
            }


        }


    }
}
