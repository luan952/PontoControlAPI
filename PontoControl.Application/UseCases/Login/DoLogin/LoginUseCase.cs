using PontoControl.Application.Services.Cryptography;
using PontoControl.Application.Services.Token;
using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;
using PontoControl.Domain.Repositories.Interfaces.Marking;
using PontoControl.Domain.Repositories.Interfaces.User;
using PontoControl.Exceptions.ExceptionsBase;

namespace PontoControl.Application.UseCases.Login.DoLogin
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IUserReadOnlyRepository _userRepositoryReadOnly;
        private readonly PasswordEncryptor _passwordEncryptor;
        private readonly TokenController _tokenController;
        private readonly IMarkingReadOnlyRepository _markingReadOnlyRepository;

        public LoginUseCase(IUserReadOnlyRepository userRepositoryReadOnly, PasswordEncryptor passwordEncryptor,
                            TokenController tokenController, IMarkingReadOnlyRepository markingReadOnlyRepository)
        {
            _userRepositoryReadOnly = userRepositoryReadOnly;
            _passwordEncryptor = passwordEncryptor;
            _tokenController = tokenController;
            _markingReadOnlyRepository = markingReadOnlyRepository;
        }

        public async Task<ResponseLogin> Execute(RequestLogin request)
        {
            var user = await _userRepositoryReadOnly.Login(request.Email, _passwordEncryptor.Encrypt(request.Password));

            if (user is null)
                throw new InvalidLoginException();

            var markingsOfDay = await _markingReadOnlyRepository.GetMarkingsOfDayByUserId(user.Id);

            return new()
            {
                Name = $"{user.FirstName} {user.LastName}",
                Token = _tokenController.TokenGenerate(user.Email),
                MarkingsOfDay = markingsOfDay
            };
        }
    }
}
