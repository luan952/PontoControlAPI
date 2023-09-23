using Microsoft.AspNetCore.Http;
using PontoControl.Application.Services.Token;
using PontoControl.Domain.Entities;
using PontoControl.Domain.Repositories.Interfaces.User;

namespace PontoControl.Application.Services.GetUserLogged
{
    public class UserLogged : IUserLogged
    {
        private readonly IHttpContextAccessor _context;
        private readonly TokenController _tokenController;
        private readonly IUserReadOnlyRepository _userRepositoryReadOnly;

        public UserLogged(IHttpContextAccessor context, TokenController tokenController, IUserReadOnlyRepository userRepositoryReadOnly)
        {
            _context = context;
            _tokenController = tokenController;
            _userRepositoryReadOnly = userRepositoryReadOnly;
        }

        public async Task<User> GetUserLogged()
        {
            var authorization = _context.HttpContext.Request.Headers["Authorization"].ToString();
            var token = authorization["Bearer".Length..].Trim();

            var userEmail = _tokenController.GetUserEmail(token);
            var user = await _userRepositoryReadOnly.GetUserByEmail(userEmail);

            return user;
        }
    }
}
