using Models.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjMongoDBPrecoBase.Service
{
    public class GetLoginUserPrecoBase
    {

        HttpClient ApiConnection = new HttpClient();
        public static async Task<BaseResponse> GetLogin(PrecoBase precobase)
        {
            var baseResponse = new BaseResponse();
            HttpClient ApiConnection = new HttpClient();

            HttpResponseMessage user = await ApiConnection.GetAsync("https://localhost:44314/api/User/GetLogin?loginUser=" + precobase.LoginUser);
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
                    baseResponse.ConnectResult(precobase);
                    return baseResponse;
                }
                else
                {
                    baseResponse.Connecterro("Usuário não tem permissão para criar Preco Base");
                    return baseResponse;
                }
            }


        }




    }
}
