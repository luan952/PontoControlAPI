using PontoControl.Application.Services.Cryptography;
using PontoControl.Application.Services.Token;
using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;
using PontoControl.Domain.Entities;
using PontoControl.Domain.Repositories.Interfaces.Collaborator;
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
        private readonly ICollaboratorReadOnlyRepository _collaboratorReadOnlyRepository;

        public LoginUseCase(IUserReadOnlyRepository userRepositoryReadOnly, PasswordEncryptor passwordEncryptor,
                            TokenController tokenController, IMarkingReadOnlyRepository markingReadOnlyRepository,
                            ICollaboratorReadOnlyRepository collaboratorReadOnlyRepository)
        {
            _userRepositoryReadOnly = userRepositoryReadOnly;
            _passwordEncryptor = passwordEncryptor;
            _tokenController = tokenController;
            _markingReadOnlyRepository = markingReadOnlyRepository;
            _collaboratorReadOnlyRepository = collaboratorReadOnlyRepository;
        }

        public async Task<ResponseLogin> Execute(RequestLogin request)
        {
            var user = await _userRepositoryReadOnly.Login(request.Email, _passwordEncryptor.Encrypt(request.Password));

            if (user is null)
                throw new InvalidLoginException();

            if (user.TypeUser == Domain.Enum.UserType.COLLABORATOR)
            {
                var collaborator = await _collaboratorReadOnlyRepository.GetCollaboratorById(user.Id);

                var markingsOfDay = await _markingReadOnlyRepository.GetMarkingsOfDayByUserId(user.Id);

                return new()
                {
                    Name = $"{user.FirstName} {user.LastName}",
                    Token = _tokenController.TokenGenerate(user.Email),
                    MarkingsOfDay = markingsOfDay,
                    Document = collaborator.Document,
                    Email = collaborator.Email,
                    IsFirstLogin = (bool)collaborator.IsFirstLogin,
                    Position = collaborator.Position,
                    TypeUser = Domain.Enum.UserType.COLLABORATOR
                };
            }

            return new()
            {
                Name = $"{user.FirstName} {user.LastName}",
                Token = _tokenController.TokenGenerate(user.Email),
                TypeUser = Domain.Enum.UserType.ADMIN,
                Email = user.Email,
                IsFirstLogin = (bool)user.IsFirstLogin
            };
        }
    }
}
