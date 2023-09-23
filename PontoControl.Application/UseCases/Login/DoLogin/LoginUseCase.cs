using PontoControl.Application.Services.Cryptography;
using PontoControl.Application.Services.Token;
using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;
using PontoControl.Domain.Repositories.Interfaces.User;
using PontoControl.Exceptions.ExceptionsBase;

namespace PontoControl.Application.UseCases.Login.DoLogin
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserReadOnlyRepository _userRepositoryReadOnly;
        private readonly PasswordEncryptor _passwordEncryptor;
        private readonly TokenController _tokenController;

        public LoginUseCase(IUserReadOnlyRepository userRepositoryReadOnly, PasswordEncryptor passwordEncryptor, TokenController tokenController)
        {
            _userRepositoryReadOnly = userRepositoryReadOnly;
            _passwordEncryptor = passwordEncryptor;
            _tokenController = tokenController;
        }

        public async Task<ResponseLogin> Execute(RequestLogin request)
        {
            var user = await _userRepositoryReadOnly.Login(request.Email, _passwordEncryptor.Encrypt(request.Password));

            return user is null
                ? throw new InvalidLoginException()
                : new()
                {
                    Name = $"{user.FirstName} {user.LastName}",
                    Token = _tokenController.TokenGenerate(user.Email)
                };
        }
    }
}
