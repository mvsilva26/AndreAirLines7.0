using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Newtonsoft.Json;
using ProjMongoDBPassagem.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ProjMongoDBPassagem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassagemController : ControllerBase
    {

        private readonly PassagemService _passagemService;

        public PassagemController(PassagemService passagemService)
        {
            _passagemService = passagemService;
        }

        [HttpGet]
        [Authorize(Roles = "adm")]
        public ActionResult<List<Passagem>> Get() =>
           _passagemService.Get();


        [HttpGet("{id:length(24)}", Name = "GetPassagem")]
        [Authorize]
        public ActionResult<Passagem> Get(string id)
        {
            var passagem = _passagemService.Get(id);

            if (passagem == null)
            {
                return NotFound();
            }

            return passagem;
        }

        [HttpPost]
        [Authorize(Roles = "adm")]
        public async Task<ActionResult<Passagem>> Create(Passagem passagem)
        {

            try
            {
                HttpClient ApiConnection = new HttpClient();
                HttpResponseMessage passageiro = await ApiConnection.GetAsync("https://localhost:44302/api/Passageiro/Search?Cpf=" + passagem.Passageiro);

                string responseBody = await passageiro.Content.ReadAsStringAsync();
                var passageiroCpf = JsonConvert.DeserializeObject<Passageiro>(responseBody);
                if (passageiroCpf.Cpf == null)
                    return NotFound("Passageiro não encontrado");
                passagem.Passageiro = passageiroCpf;

                HttpResponseMessage voo = await ApiConnection.GetAsync("https://localhost:44367/api/Voo/Search?origin=" + passagem.Voo.Origem.Sigla + "&destination=" + passagem.Voo.Destino.Sigla);

                string responseBody1 = await voo.Content.ReadAsStringAsync();
                var vooBusca = JsonConvert.DeserializeObject<Voo>(responseBody1);
                if (vooBusca.Destino.Sigla == null || vooBusca.Origem.Sigla == null)
                    return NotFound("Voo não encontrado");
                passagem.Voo = vooBusca;

                HttpResponseMessage precoBase = await ApiConnection.GetAsync("https://localhost:44375/api/PrecoBase/Search?origin=" + passagem.Voo.Origem.Sigla + "&destination=" + passagem.Voo.Destino.Sigla);

                string responseBody2 = await voo.Content.ReadAsStringAsync();
                var precoBaseObj = JsonConvert.DeserializeObject<PrecoBase>(responseBody2);
                if (precoBaseObj.Destino.Sigla == null && precoBaseObj.Origem.Sigla == null)
                    return NotFound("Preco Base não encontrado");
                passagem.PrecoBase = precoBaseObj;

                passagem.TotalPreco = (passagem.PrecoBase.Preco + passagem.Classe.Valor) - (((passagem.PrecoBase.Preco + passagem.Classe.Valor) * passagem.PromocaoPorcentagem) / 100);




            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }


            var responseGetLogin = await GetLoginUser.GetLogin(passagem);

            if (responseGetLogin.Sucess == true)
            {
                _passagemService.Create(passagem);


                var passagemJson = JsonConvert.SerializeObject(passagem);
                PostLogAPI.PostLog(new Log(passagem.LoginUser, null, passagemJson, "Create"));



                return CreatedAtRoute("GetPassagem", new { id = passagem.Id.ToString() }, passagem);
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
        [Authorize(Roles = "UpdatePassagem")]
        public IActionResult Update(string id, Passagem passagemIn)
        {
            var passagem = _passagemService.Get(id);

            if (passagem == null)
            {
                return NotFound();
            }


            var passagemJson = JsonConvert.SerializeObject(passagem);
            var passagemInJson = JsonConvert.SerializeObject(passagemIn);
            PostLogAPI.PostLog(new Log(passagemIn.LoginUser, passagemJson, passagemInJson, "UpDate"));

            _passagemService.Update(id, passagemIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "DeletePassagem")]
        public IActionResult Delete(string id)
        {
            var passagem = _passagemService.Get(id);

            if (passagem == null)
            {
                return NotFound();
            }


            var passagemJson = JsonConvert.SerializeObject(passagem);
            PostLogAPI.PostLog(new Log(passagem.LoginUser, passagemJson, null, "Delete"));


            _passagemService.Remove(passagem.Id);

            return NoContent();
        }




    }
}
