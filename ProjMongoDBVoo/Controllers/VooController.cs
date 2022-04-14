using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Newtonsoft.Json;
using ProjMongoDBVoo.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjMongoDBVoo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VooController : ControllerBase
    {

        private readonly VooService _vooService;


        public VooController(VooService vooService)
        {
            _vooService = vooService;
        }

        [HttpGet]
        public ActionResult<List<Voo>> Get() =>
           _vooService.Get();


        [HttpGet("Search")]
        public ActionResult<Voo> GetSiglaAeroporto(string origin, string destination)
        {
            var voo = _vooService.GetVoo(origin, destination);
            if (voo == null)
            {
                return NotFound();
            }
            return
                voo;
        }



        [HttpGet("{id:length(24)}", Name = "GetVoo")]
        public ActionResult<Voo> Get(string id)
        {
            var voo = _vooService.Get(id);

            if (voo == null)
            {
                return NotFound();
            }

            return voo;
        }


        [HttpPost]
        public async Task<ActionResult<Voo>> Create(Voo voo)
        {

            try
            {
                HttpClient ApiConnection = new HttpClient();
                HttpResponseMessage aeroporto = await ApiConnection.GetAsync("https://localhost:44364/api/Aeroporto/GetSigla?Sigla=" + voo.Origem.Sigla); ;
                var status = aeroporto.StatusCode;

                string responseBody = await aeroporto.Content.ReadAsStringAsync();

                var aeroportoOrigem = JsonConvert.DeserializeObject<Aeroporto>(responseBody);
                if (aeroportoOrigem.Sigla == null)
                    return NotFound("Aeroporto não encontrado");
                voo.Origem = aeroportoOrigem;

                aeroporto = await ApiConnection.GetAsync("https://localhost:44364/api/Aeroporto/GetSigla?Sigla=" + voo.Destino.Sigla);

                responseBody = await aeroporto.Content.ReadAsStringAsync();
                var aeroportoDestino = JsonConvert.DeserializeObject<Aeroporto>(responseBody);
                if (aeroportoDestino.Sigla == null)
                    return NotFound("Aeroporto não encontrado");
                voo.Destino = aeroportoDestino;

                HttpResponseMessage aeronave = await ApiConnection.GetAsync("https://localhost:44353/api/Aeronave/Search?placa=" + voo.Aeronave);

                string responseBody1 = await aeronave.Content.ReadAsStringAsync();
                var aeronaveCod = JsonConvert.DeserializeObject<Aeronave>(responseBody1);
                if (aeronaveCod.Placa == null)
                    return NotFound("Aeronave não encontrada");
                voo.Aeronave = aeronaveCod;


            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }

            var responseGetLogin = await GetLoginUser.GetLogin(voo);

            if (responseGetLogin.Sucess == true)
            {
                _vooService.Create(voo);

                var vooJson = JsonConvert.SerializeObject(voo);
                PostLogAPI.PostLog(new Log(voo.LoginUser, null, vooJson, "Create"));

                return CreatedAtRoute("GetVoo", new { id = voo.Id.ToString() }, voo);
            }
            else
            {
                return GetResponse(responseGetLogin);
            }

        }

        private ActionResult GetResponse(BaseResponse baseResponse)
        {
            if (baseResponse.Sucess == true)
            {
                return Ok(baseResponse.Result);
            }
            return BadRequest(baseResponse.Erros);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Voo vooIn)
        {
            var voo = _vooService.Get(id);

            if (voo == null)
            {
                return NotFound();
            }


            var vooJson = JsonConvert.SerializeObject(voo);
            var vooInJson = JsonConvert.SerializeObject(vooIn);
            PostLogAPI.PostLog(new Log(vooIn.LoginUser, vooJson, vooInJson, "UpDate"));



            _vooService.Update(id, vooIn);

            return NoContent();
        }



        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var voo = _vooService.Get(id);

            if (voo == null)
            {
                return NotFound();
            }

            var voojson = JsonConvert.SerializeObject(voo);
            PostLogAPI.PostLog(new Log(voo.LoginUser, voojson, null, "Delete"));


            _vooService.Remove(voo.Id);

            return NoContent();
        }

    }
}
