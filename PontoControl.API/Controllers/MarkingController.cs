using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using PontoControl.API.Filters;
using PontoControl.Application.UseCases.Marking.DowloadMarkings;
using PontoControl.Application.UseCases.Marking.GetByUser;
using PontoControl.Application.UseCases.Marking.GetOfDay;
using PontoControl.Application.UseCases.Marking.Register;
using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;
using PontoControl.Domain.Repositories.Interfaces.Marking;

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

        [HttpPut]
        [ProducesResponseType(typeof(GetMarkingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(UserAuthenticatedAttribute))]
        public async Task<IActionResult> GetMarkingsByUser([FromServices] IGetMarkingsUseCase useCase, [FromBody] GetMarkingByUserRequest request)
        {
            var result = await useCase.Execute(request);

            if (!result.Any())
                return NoContent();

            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(MarkingOfDayResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(UserAuthenticatedAttribute))]
        public async Task<IActionResult> GetMarkingsOfDayByUser([FromServices] IGetMarkingOfDayUseCase useCase)
        {
            var result = await useCase.Execute();

            if (!result.MarkingsOfDay.Marking.Any())
                return NoContent();

            return Ok(result);
        }

        [HttpPut("download-markings")]
        public async Task<IActionResult> DownloadExcel([FromServices] IDowloadMarkings useCase, [FromBody] DowloadMarkingRequest request)
        {
            var fileStream = await useCase.Execute(request);
            return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "exemplo.xlsx");
        }
    }
}
