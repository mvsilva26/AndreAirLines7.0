using Models.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ServiceAPI
{
    public class ViaCep
    {

        static readonly HttpClient client = new HttpClient();
        public static async Task<Endereco> GetEndereco(string cep)
        {
            try
            {
                HttpResponseMessage endereco = await client.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                endereco.EnsureSuccessStatusCode();
                string responseBody = await endereco.Content.ReadAsStringAsync();
                var enderecoObj = JsonConvert.DeserializeObject<Endereco>(responseBody);
                return enderecoObj;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }
        }


    }
}
