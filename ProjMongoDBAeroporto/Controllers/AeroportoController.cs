using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Newtonsoft.Json;
using ProjMongoDBAeroporto.Service;
using ServiceAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjMongoDBAeroporto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeroportoController : ControllerBase
    {

        private readonly AeroportoService _aeroportoService;

        public AeroportoController(AeroportoService aeroportoService)
        {
            _aeroportoService = aeroportoService;
        }

        [HttpGet]
        [Authorize(Roles = "adm")]
        public ActionResult<List<Aeroporto>> Get() =>
           _aeroportoService.Get();



        [HttpGet("GetSigla")]
        [Authorize]
        public ActionResult<Aeroporto> GetSiglaAeroporto(string Sigla)                  ///////////
        {                                                                               ///////////
            var aeroporto = _aeroportoService.GetSigla(Sigla);
            if (aeroporto == null)
            {
                return NotFound();
            }
            return
                aeroporto;
        }



        [HttpGet("{id:length(24)}", Name = "GetAeroporto")]
        [Authorize(Roles = "GetAeroportoId,adm")]
        public ActionResult<Aeroporto> Get(string id)
        {
            var aeroporto = _aeroportoService.Get(id);

            if (aeroporto == null)
            {
                return NotFound();
            }

            return aeroporto;
        }


        [HttpPost]
        [Authorize(Roles = "adm")]
        public async Task <ActionResult<Aeroporto>> Create(Aeroporto aeroporto)
        {

            var enderecoAPI = await ViaCep.GetEndereco(aeroporto.Endereco.Cep);
            aeroporto.Endereco = new Endereco(enderecoAPI.Logradouro, enderecoAPI.Uf, enderecoAPI.Localidade, enderecoAPI.Bairro, aeroporto.Endereco.Numero);



            var responseGetLogin = await GetLoginUser.GetLogin(aeroporto);

            if (responseGetLogin.Sucess == true)
            {
                _aeroportoService.Create(aeroporto);


                var aeroportoJson = JsonConvert.SerializeObject(aeroporto);
                PostLogAPI.PostLog(new Log(aeroporto.LoginUser, null, aeroportoJson, "Create"));



                return CreatedAtRoute("GetAeroporto", new { id = aeroporto.Id.ToString() }, aeroporto);
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
        [Authorize(Roles = "UpdateAeroporto")]
        public IActionResult Update(string id, Aeroporto aeroportoIn)
        {
            var aeroporto = _aeroportoService.Get(id);

            if (aeroporto == null)
            {
                return NotFound();
            }

            var aeroportoJson = JsonConvert.SerializeObject(aeroporto);
            var aeronaveInJson = JsonConvert.SerializeObject(aeroportoIn);
            PostLogAPI.PostLog(new Log(aeroportoIn.LoginUser, aeroportoJson, aeronaveInJson, "UpDate"));


            _aeroportoService.Update(id, aeroportoIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "DeleteAeroporto")]
        public IActionResult Delete(string id)
        {
            var aeroporto = _aeroportoService.Get(id);

            if (aeroporto == null)
            {
                return NotFound();
            }


            var aeroportoJson = JsonConvert.SerializeObject(aeroporto);
            PostLogAPI.PostLog(new Log(aeroporto.LoginUser, aeroportoJson, null, "Delete"));

            _aeroportoService.Remove(aeroporto.Id);

            return NoContent();
        }











    }
}
