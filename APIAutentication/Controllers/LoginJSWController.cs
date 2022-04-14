using AuthenticationJWS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using System.Threading.Tasks;

namespace AuthenticationJWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginJSWController : ControllerBase
    {

        //[HttpPost]
        //[Route("login")]
        //public async Task<ActionResult<dynamic>> AuthenticateAsync(User model)
        //{
        //    var user = await ServiceSeachApiExisting.SeachUserInApiByLoginUser(model.LoginUser);

        //    if (user == null)
        //        return NotFound("User or password invalid, try again");

        //    var token = TokenService.GenerateToken(user);

        //    user.Password = "";

        //    return new
        //    {
        //        user = user,
        //        token = token
        //    };
        //}

        


    }
}
