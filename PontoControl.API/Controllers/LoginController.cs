using Microsoft.AspNetCore.Mvc;
using PontoControl.Application.UseCases.Login.DoLogin;
using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;

namespace PontoControl.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController  : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseLogin), StatusCodes.Status201Created)]
        public async Task<IActionResult> Login([FromServices] ILoginUseCase useCase, [FromBody] RequestLogin request)
        {
            var user = await useCase.Execute(request);
            return Ok(user);
        }
    }
}
