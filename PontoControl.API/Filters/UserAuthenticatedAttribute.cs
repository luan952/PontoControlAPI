using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using PontoControl.Application.Services.Token;
using PontoControl.Comunication.Responses;
using PontoControl.Domain.Repositories.Interfaces.User;
using PontoControl.Exceptions;
using PontoControl.Exceptions.ExceptionsBase;

namespace PontoControl.API.Filters
{
    public class UserAuthenticatedAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly TokenController _tokenController;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        public UserAuthenticatedAttribute(TokenController tokenController, IUserReadOnlyRepository userReadOnlyRepository)
        {
            _tokenController = tokenController;
            _userReadOnlyRepository = userReadOnlyRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                var token = TokenInRequest(context);

                var userEmail = _tokenController.GetUserEmail(token);

                var user = await _userReadOnlyRepository.IsEmailExists(userEmail);

                if (!user)
                {
                    throw new PontoControlException(string.Empty);
                }
            }
            catch (SecurityTokenExpiredException)
            {
                ExpiredToken(context);
            }
            catch
            {
                UnauthorizedAccess(context);
            }
        }

        public static string TokenInRequest(AuthorizationFilterContext context)
        {
            var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrWhiteSpace(authorization))
            {
                throw new PontoControlException(string.Empty);
            }

            return authorization["Bearer".Length..].Trim();
        }

        private static void ExpiredToken(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse(ResourceMessageError.expired_token));
        }

        private static void UnauthorizedAccess(AuthorizationFilterContext context)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse(ResourceMessageError.unauthorized_token));
        }
    }
}
