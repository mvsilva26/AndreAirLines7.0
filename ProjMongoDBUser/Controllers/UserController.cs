using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Newtonsoft.Json;
using ProjMongoDBUser.Service;
using ServiceAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjMongoDBUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
/*
        [HttpGet]
        [Authorize(Roles = "GetUser")]
        public ActionResult<List<User>> Get() =>
            _userService.Get();

        [HttpGet("Search")]
        [Authorize(Roles = "GetUserCpf")]
        public ActionResult<User> GetUserCpf(string cpf)
        {
            var user = _userService.GetCpf(cpf);
            if (user == null)
            {
                return NotFound();
            }
            return
                user;
        }
*/
        [HttpPost("GetLogin")]
        [AllowAnonymous]
        public ActionResult<User> GetLogin(User u)
        {
            var user = _userService.GetLogin(u);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }



        [HttpGet("{id:length(24)}", Name = "GetUser")]
        [AllowAnonymous]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Create(User user)
        {

           // var EnderecoApi = await ViaCep.GetEndereco(user.Endereco.Cep);
           // user.Endereco = new Endereco(EnderecoApi.Logradouro, EnderecoApi.Uf, EnderecoApi.Localidade, EnderecoApi.Bairro, user.Endereco.Numero, user.Endereco.Complemento, EnderecoApi.Cep);


          //  if (!CpfService.CheckCpfDB(user.Cpf, _userService))
          //      return null;


          //  var responseGetLogin = await ConferirLoginUser.GetLogin(user);

            //if (responseGetLogin.Sucess == true)
            //{

                _userService.Create(user);

                //var userJson = JsonConvert.SerializeObject(user);
                //PostLogAPI.PostLog(new Log(user.LoginUser, null, userJson, "Create"));

                return CreatedAtRoute("GetUser", new { id = user.Id.ToString() }, user);
            //}
            //else
            //{
            //    return GetResponse(responseGetLogin);
            //}
        }

        private ActionResult GetResponse(BaseResponse baseResponse)
        {
            if (baseResponse.Sucess == true)
            {
                return Ok(baseResponse.Result);
            }
            return BadRequest(baseResponse.Erros);
        }
/*


        [HttpPut("{id:length(24)}")]
        [Authorize(Roles = "UpdateUser")]
        public IActionResult Update(string id, User userIn)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }


            var userJson = JsonConvert.SerializeObject(user);
            var userInJson = JsonConvert.SerializeObject(userIn);
            PostLogAPI.PostLog(new Log(userIn.LoginUser, userJson, userInJson, "UpDate"));

            _userService.Update(id, userIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "DeleteUser")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            var userJson = JsonConvert.SerializeObject(user);
            PostLogAPI.PostLog(new Log(user.LoginUser, userJson, null, "Delete"));


            _userService.Remove(user.Id);

            return NoContent();
        }*/
    }
}
