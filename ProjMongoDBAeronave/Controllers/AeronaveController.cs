using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Newtonsoft.Json;
using ProjMongoDBAeronave.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjMongoDBAeronave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AeronaveController : ControllerBase
    {

        //[HttpPost]
        //[Route("login")]
        //[AllowAnonymous]
        //public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        //{
        //    // Recupera o usuário
        //    var user = await GetUser.GetLogin(model.Login, model.Senha);

        //    // Verifica se o usuário existe
        //    if (user == null)
        //        return NotFound(new { message = "Usuário ou senha inválidos" });

        //    // Gera o Token
        //    var token = TokenService.GenerateToken(user);

        //    // Oculta a senha
        //    user.Senha = "";

        //    // Retorna os dados
        //    return new
        //    {
        //        user = user,
        //        token = token
        //    };
        //}


        private readonly AeronaveService _aeronaveService;

        public AeronaveController(AeronaveService aeronaveService)
        {
            _aeronaveService = aeronaveService;
        }

        [HttpGet]
        [Authorize(Roles = "adm")]
        public ActionResult<List<Aeronave>> Get() =>
           _aeronaveService.Get();


        [HttpGet("SearchPlaca")]
        [Authorize]
        public ActionResult<Aeronave> GetAeronavePlaca(string placa)
        {
            var aeronave = _aeronaveService.GetAeronavePlaca(placa);
            if (aeronave == null)
            {
                return NotFound();
            }
            return
                aeronave;
        }



        [HttpGet("{id:length(24)}", Name = "GetAeronave")]
        [Authorize(Roles = "GetAeronaveId,adm")]
        public ActionResult<Aeronave> Get(string id)
        {
            var aeronave = _aeronaveService.Get(id);

            if (aeronave == null)
            {
                return NotFound();
            }

            return aeronave;
        }

        [HttpPost]
        [Authorize(Roles = "adm")]
        public async Task<ActionResult<Aeronave>> Create(Aeronave aeronave)
        {

            var responseGetLogin = await GetLoginUser.GetLogin(aeronave);

            if (responseGetLogin.Sucess == true)
            {
                _aeronaveService.Create(aeronave);

                var aeronaveJson = JsonConvert.SerializeObject(aeronave);
                PostLogAPI.PostLog(new Log(aeronave.LoginUser, null, aeronaveJson, "Create"));

                return CreatedAtRoute("GetAeronave", new { id = aeronave.Id.ToString() }, aeronave);
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
        [Authorize(Roles = "UpdateAeronave")]
        public IActionResult Update(string id, Aeronave aeronaveIn)
        {
            var aeronave = _aeronaveService.Get(id);

            if (aeronave == null)
            {
                return NotFound();
            }



            var aeronaveJson = JsonConvert.SerializeObject(aeronave);
            var aeronaveInJson = JsonConvert.SerializeObject(aeronaveIn);
            PostLogAPI.PostLog(new Log(aeronaveIn.LoginUser, aeronaveJson, aeronaveInJson, "UpDate"));



            _aeronaveService.Update(id, aeronaveIn);

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "DeleteAeronave")]
        public IActionResult Delete(string id)
        {
            var aeronave = _aeronaveService.Get(id);

            if (aeronave == null)
            {
                return NotFound();
            }

            var aeronaveJson = JsonConvert.SerializeObject(aeronave);
            PostLogAPI.PostLog(new Log(aeronave.LoginUser, aeronaveJson, null, "Delete"));

            _aeronaveService.Remove(aeronave.Id);

            return NoContent();
        }

    }
}
