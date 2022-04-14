using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Newtonsoft.Json;
using ProjMongoDBPrecoBase.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjMongoDBPrecoBase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasseController : ControllerBase
    {

        private readonly ClasseService _classeService;

        public ClasseController(ClasseService classeService)
        {
            _classeService = classeService;
        }

        [HttpGet]
        [Authorize(Roles = "adm")]
        public ActionResult<List<Classe>> Get() =>
           _classeService.Get();


        [HttpGet("{id:length(24)}", Name = "GetClasse")]
        [Authorize]
        public ActionResult<Classe> Get(string id)
        {
            var classe = _classeService.Get(id);

            if (classe == null)
            {
                return NotFound();
            }

            return classe;
        }

        [HttpPost]
        [Authorize(Roles = "adm")]
        public async Task<ActionResult<Classe>> Create(Classe classe)
        {


            var responseGetLogin = await GetLoginUserClasse.GetLogin(classe);


            if (responseGetLogin.Sucess == true)
            {
                _classeService.Create(classe);


                var classeJson = JsonConvert.SerializeObject(classe);
                PostLogAPI.PostLog(new Log(classe.LoginUser, null, classeJson, "Create"));

                return CreatedAtRoute("GetClasse", new { id = classe.Id.ToString() }, classe);
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
        [Authorize(Roles = "UpdateClasse")]
        public IActionResult Update(string id, Classe classeIn)
        {
            var classe = _classeService.Get(id);

            if (classe == null)
            {
                return NotFound();
            }


            var classeJson = JsonConvert.SerializeObject(classe);
            var classeInJson = JsonConvert.SerializeObject(classeIn);
            PostLogAPI.PostLog(new Log(classeIn.LoginUser, classeJson, classeInJson, "UpDate"));


            _classeService.Update(id, classeIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "DeleteClasse")]
        public IActionResult Delete(string id)
        {
            var classe = _classeService.Get(id);

            if (classe == null)
            {
                return NotFound();
            }

            _classeService.Remove(classe.Id);

            return NoContent();
        }




    }
}
