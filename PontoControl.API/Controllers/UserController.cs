using Microsoft.AspNetCore.Mvc;
using PontoControl.API.Filters;
using PontoControl.Application.UseCases.User.RegisterCollaborator;
using PontoControl.Comunication.Requests;

namespace PontoControl.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RegisterCollaboratorRequest), StatusCodes.Status201Created)]
        //[ServiceFilter(typeof(UserAuthenticatedAttribute))]
        public async Task<IActionResult> RegisterCollaborator([FromServices] IRegisterCollaboratorUseCase useCase, [FromBody] RegisterCollaboratorRequest request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }
    }
}
