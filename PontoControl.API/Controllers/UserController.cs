﻿using Microsoft.AspNetCore.Mvc;
using PontoControl.API.Filters;
using PontoControl.Application.UseCases.User.RegisterCollaborator;
using PontoControl.Application.UseCases.User.UpdatePassword;
using PontoControl.Application.UseCases.User.UpdatePasswordNoLogged;
using PontoControl.Comunication.Requests;

namespace PontoControl.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(RegisterCollaboratorRequest), StatusCodes.Status201Created)]
        [ServiceFilter(typeof(UserAuthenticatedAttribute))]
        public async Task<IActionResult> RegisterCollaborator([FromServices] IRegisterCollaboratorUseCase useCase, [FromBody] RegisterCollaboratorRequest request)
        {
            var result = await useCase.Execute(request);
            return Created(string.Empty, result);
        }

        [HttpPut]
        [Route("update-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(UserAuthenticatedAttribute))]
        public async Task<IActionResult> UpdatePassword([FromServices] IUpdatePasswordUseCase useCase, [FromBody] UpdatePasswordRequest request)
        {
            await useCase.Execute(request);
            return NoContent();
        }

        [HttpPut]
        [Route("update-password-no-logged")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePasswordNoLogged([FromServices] IUpdatePasswordNoLoggedUseCase useCase, [FromBody] UpdatePasswordNoLoggedRequest request)
        {
            await useCase.Execute(request);
            return NoContent();
        }
    }
}
