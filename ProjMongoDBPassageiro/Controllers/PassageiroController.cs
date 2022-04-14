using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Newtonsoft.Json;
using ProjMongoDBPassageiro.Service;
using ServiceAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjMongoDBPassageiro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassageiroController : ControllerBase
    {

        private readonly PassageiroService _passageiroService;

        public PassageiroController(PassageiroService passageiroService)
        {
            _passageiroService = passageiroService;
        }

        [HttpGet]
        [Authorize(Roles = "adm")]
        public ActionResult<List<Passageiro>> Get() =>
           _passageiroService.Get();


        [HttpGet("Search")]
        [Authorize]
        public ActionResult<Passageiro> GetPassageiroCpf(string Cpf)
        {
            var passageiro = _passageiroService.GetCpf(Cpf);
            if (passageiro == null)
            {
                return NotFound();
            }
            return
                passageiro;
        }



        [HttpGet("{id:length(24)}", Name = "GetPassageiro")]
        [Authorize(Roles = "GetPassageiro,adm")]
        public ActionResult<Passageiro> Get(string id)
        {
            var passageiro = _passageiroService.Get(id);

            if (passageiro == null)
            {
                return NotFound();
            }

            return passageiro;
        }


        [HttpPost]
        [Authorize(Roles = "adm")]
        public async Task<ActionResult<Passageiro>> Create(Passageiro passageiro)
        {

            var enderecoAPI = await ViaCep.GetEndereco(passageiro.Endereco.Cep);
            passageiro.Endereco = new Endereco(enderecoAPI.Logradouro, enderecoAPI.Uf, enderecoAPI.Localidade, enderecoAPI.Bairro, passageiro.Endereco.Numero);


            if (!CpfService.CheckCpfDB(passageiro.Cpf, _passageiroService))
                return null;

            var responseGetLogin = await GetLoginUser.GetLogin(passageiro);

            if (responseGetLogin.Sucess == true)
            {
                _passageiroService.Create(passageiro);


                var passageiroJson = JsonConvert.SerializeObject(passageiro);
                PostLogAPI.PostLog(new Log(passageiro.LoginUser, null, passageiroJson, "Create"));


                return CreatedAtRoute("GetPassageiro", new { id = passageiro.Id.ToString() }, passageiro);
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
        public IActionResult Update(string id, Passageiro passageiroIn)
        {
            var passageiro = _passageiroService.Get(id);

            if (passageiro == null)
            {
                return NotFound();
            }


            var passageiroJson = JsonConvert.SerializeObject(passageiro);
            var passageiroInJson = JsonConvert.SerializeObject(passageiroIn);
            PostLogAPI.PostLog(new Log(passageiroIn.LoginUser, passageiroJson, passageiroInJson, "UpDate"));
            _passageiroService.Update(id, passageiroIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "DeletePassageiro")]
        public IActionResult Delete(string id)
        {
            var passageiro = _passageiroService.Get(id);

            if (passageiro == null)
            {
                return NotFound();
            }


            var passageiroJson = JsonConvert.SerializeObject(passageiro);
            PostLogAPI.PostLog(new Log(passageiro.LoginUser, passageiroJson, null, "Delete"));

            _passageiroService.Remove(passageiro.Id);


            return NoContent();
        }

    }
}
