using Models.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjMongoDBVoo.Service
{
    public class GetLoginUser
    {

        HttpClient ApiConnection = new HttpClient();
        public static async Task<BaseResponse> GetLogin(Voo voo)
        {
            var baseResponse = new BaseResponse();
            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44314/api/User/GetLogin?loginUser=" + voo.LoginUser);
            string responseBody = await user.Content.ReadAsStringAsync();

            var userLogin = JsonConvert.DeserializeObject<User>(responseBody);

            if (userLogin.Login == null)
            {
                baseResponse.Connecterro("User not found");
                return baseResponse;
            }
            else
            {
                if (userLogin.Function.Id == "1" || userLogin.Function.Id == "2")
                {
                    baseResponse.ConnectResult(voo);
                    return baseResponse;
                }
                else
                {
                    baseResponse.Connecterro("User without permission for a create a Ticket");
                    return baseResponse;
                }
            }


        }




    }
}
