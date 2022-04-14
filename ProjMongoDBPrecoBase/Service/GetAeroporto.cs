using Models.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjMongoDBPrecoBase.Service
{
    public class GetAeroporto
    {
        //static readonly HttpClient client = new HttpClient();
        //public static async Task<BaseResponse> GetAeroportoApi(PrecoBase precobase)
        //{
        //    var baseResponse = new BaseResponse();

        //    HttpClient ApiConnection = new HttpClient();
        //    HttpResponseMessage aeroporto = await ApiConnection.GetAsync("https://localhost:44364/api/Aeroporto/GetSigla?Sigla=" + precobase.Origem.Sigla);

        //    string responseBody = await aeroporto.Content.ReadAsStringAsync();
        //    var aeroportoOrigem = JsonConvert.DeserializeObject<Aeroporto>(responseBody);
        //    if (aeroportoOrigem.Sigla == null)
        //    {
        //        baseResponse.Connecterro("Aeroporto de origem não encontrado");
        //        return baseResponse;
        //    }
        //    else
        //    {
        //        precobase.Origem = aeroportoOrigem;
        //    }




        //    aeroporto = await ApiConnection.GetAsync("https://localhost:44364/api/Aeroporto/GetSigla?Sigla=" + precobase.Destino.Sigla);

        //    responseBody = await aeroporto.Content.ReadAsStringAsync();
        //    var aeroportoDestino = JsonConvert.DeserializeObject<Aeroporto>(responseBody);


        //    if (aeroportoDestino.Sigla == null)
        //    {
        //        baseResponse.Connecterro("Aeroporto de destino não encontrado");
        //        return baseResponse;
        //    }
        //    else
        //    {
        //        precobase.Destino = aeroportoDestino;
        //    }
        //}
    }
}
