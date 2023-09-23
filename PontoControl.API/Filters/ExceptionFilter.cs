using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PontoControl.Comunication.Responses;
using PontoControl.Exceptions;
using PontoControl.Exceptions.ExceptionsBase;
using System.Net;

namespace PontoControl.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is PontoControlException)
                HandlePontoControlException(context);
            else
                HandleUnknowError(context);
        }

        private static void HandlePontoControlException(ExceptionContext context)
        {
            if (context.Exception is ValidatorErrorException)
                HandleValidatorErrorException(context);
            else if (context.Exception is InvalidLoginException)
                HandleInvalidLoginException(context);
        }

        private static void HandleValidatorErrorException(ExceptionContext context)
        {
            ValidatorErrorException? exception = context.Exception as ValidatorErrorException;

            if (exception != null)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new ObjectResult(new ErrorResponse(exception.ErrorMessages));
            }
        }

        private static void HandleInvalidLoginException(ExceptionContext context)
        {
            var loginError = context.Exception as InvalidLoginException;

            if (loginError is not null)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Result = new ObjectResult(new ErrorResponse(loginError.Message));
            }
        }

        private static void HandleUnknowError(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ErrorResponse(ResourceMessageError.unknow_error));
        }
    }
}
