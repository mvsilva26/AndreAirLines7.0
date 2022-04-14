using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjMongoDBPrecoBase.Service
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrecoBaseController : ControllerBase
    {
        private readonly PrecoBaseService _precoBaseService;


        public PrecoBaseController(PrecoBaseService precoBaseService)
        {
            _precoBaseService = precoBaseService;
        }

        [HttpGet]
        [Authorize(Roles = "adm")]
        public ActionResult<List<PrecoBase>> Get() =>
           _precoBaseService.Get();


        [HttpGet("Search")]
        [Authorize]
        public ActionResult<PrecoBase> GetPrecoBase(string origin, string destination)
        {
            var precoBase = _precoBaseService.GetPrecoBase(origin, destination);
            if (precoBase == null)
            {
                return NotFound();
            }
            return
                precoBase;
        }




        [HttpGet("{id:length(24)}", Name = "GetPrecoBase")]
        [Authorize(Roles = "adm")]
        public ActionResult<PrecoBase> Get(string id)
        {
            var precobase = _precoBaseService.Get(id);

            if (precobase == null)
            {
                return NotFound();
            }

            return precobase;
        }

            
        [HttpPost]
        [Authorize(Roles = "adm")]
        public async Task<ActionResult<PrecoBase>> Create(PrecoBase precobase)
        {

            try
            {
                HttpClient ApiConnection = new HttpClient();
                HttpResponseMessage aeroporto = await ApiConnection.GetAsync("https://localhost:44364/api/Aeroporto/GetSigla?Sigla=" + precobase.Origem.Sigla);

                string responseBody = await aeroporto.Content.ReadAsStringAsync();
                var aeroportoOrigem = JsonConvert.DeserializeObject<Aeroporto>(responseBody);
                if (aeroportoOrigem.Sigla == null)
                    return NotFound("Aeroporto de origem não encontrado");
                precobase.Origem = aeroportoOrigem;

                aeroporto = await ApiConnection.GetAsync("https://localhost:44364/api/Aeroporto/GetSigla?Sigla=" + precobase.Destino.Sigla);

                responseBody = await aeroporto.Content.ReadAsStringAsync();
                var aeroportoDestino = JsonConvert.DeserializeObject<Aeroporto>(responseBody);
                if (aeroportoDestino.Sigla == null)
                    return NotFound("Aeroporto de destino não encontrado");
                precobase.Destino = aeroportoDestino;


            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                return Problem("Problema com conexão Aeroporto API");
            }

            var responseGetLogin = await GetLoginUserPrecoBase.GetLogin(precobase);

            if (responseGetLogin.Sucess == true)
            {
                _precoBaseService.Create(precobase);

                var precobaseJson = JsonConvert.SerializeObject(precobase);
                PostLogAPI.PostLog(new Log(precobase.LoginUser, null, precobaseJson, "Create"));


                return CreatedAtRoute("GetPrecoBase", new { id = precobase.Id.ToString() }, precobase);
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
        [Authorize(Roles = "UpdatePrecoBase")]
        public IActionResult Update(string id, PrecoBase precobaseIn)
        {
            var precobase = _precoBaseService.Get(id);

            if (precobase == null)
            {
                return NotFound();
            }

            _precoBaseService.Update(id, precobaseIn);


            var precobaseJson = JsonConvert.SerializeObject(precobase);
            var precobaseInJson = JsonConvert.SerializeObject(precobaseIn);
            PostLogAPI.PostLog(new Log(precobaseIn.LoginUser, precobaseJson, precobaseInJson, "UpDate"));




            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "DeletePrecoBase")]
        public IActionResult Delete(string id)
        {
            var precobase = _precoBaseService.Get(id);

            if (precobase == null)
            {
                return NotFound();
            }

            _precoBaseService.Remove(precobase.Id);

            var precobaseJson = JsonConvert.SerializeObject(precobase);
            PostLogAPI.PostLog(new Log(precobase.LoginUser, precobaseJson, null, "Delete"));



            return NoContent();
        }
    }
}
