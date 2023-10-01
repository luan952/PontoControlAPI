using Microsoft.AspNetCore.Mvc;
using PontoControl.API.Filters;
using PontoControl.Application.UseCases.Marking.Register;
using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;

namespace PontoControl.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarkingController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RegisterMarkingResponse), StatusCodes.Status201Created)]
        [ServiceFilter(typeof(UserAuthenticatedAttribute))]
        public async Task<IActionResult> RegisterMarking([FromServices] IRegisterMakingUseCase useCase, [FromBody] RegisterMarkingRequest request)
        {
            var markingsOfDay = await useCase.Execute(request);
            return Ok(markingsOfDay);
        }
    }
}
