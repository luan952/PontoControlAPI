using AutoMapper;
using PontoControl.Application.Services.Cryptography;
using PontoControl.Application.Services.GetUserLogged;
using PontoControl.Application.Services.Token;
using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;
using PontoControl.Domain.Entities;
using PontoControl.Domain.Repositories;
using PontoControl.Domain.Repositories.Interfaces.Collaborator;
using PontoControl.Domain.Repositories.Interfaces.User;
using PontoControl.Exceptions;
using PontoControl.Exceptions.ExceptionsBase;

namespace PontoControl.Application.UseCases.User.RegisterCollaborator
{
    public class RegisterCollaboratorUseCase : IRegisterCollaboratorUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly ICollaboratorWriteOnlyRepository _collaboratorWriteOnlyRepository;
        private readonly IMapper _mapper;
        private readonly PasswordEncryptor _passwordEncryptor;
        private readonly TokenController _token;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IUserLogged _userLogged;

        public RegisterCollaboratorUseCase(ICollaboratorWriteOnlyRepository collaboratorWriteOnlyRepository,
                                           IUserReadOnlyRepository userReadOnlyRepository,
                                           IMapper mapper, PasswordEncryptor passwordEncryptor,
                                           TokenController token, IUnityOfWork unityOfWork,
                                           IUserLogged userLogged)
        {
            _collaboratorWriteOnlyRepository = collaboratorWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
            _passwordEncryptor = passwordEncryptor;
            _token = token;
            _unityOfWork = unityOfWork;
            _userLogged = userLogged;
        }

        public async Task<RegisterCollaboratorResponse> Execute(RegisterCollaboratorRequest request)
        {
            await Validate(request);

            var user = _mapper.Map<Domain.Entities.Collaborator>(request);
            user.Password = _passwordEncryptor.Encrypt(request.Password);
            user.TypeUser = Domain.Enum.UserType.COLLABORATOR;

            var userLogged = await _userLogged.GetUserLogged();
            user.AdminId = userLogged.Id;

            await _collaboratorWriteOnlyRepository.InsertCollaborator(user);
            await _unityOfWork.Commit();

            return new()
            {
                Token = _token.TokenGenerate(user.Email)
            };
        }

        public async Task Validate(RegisterCollaboratorRequest request)
        {
            var validate = new RegisterCollaboratorValidator();
            var result = validate.Validate(request);

            var emailExists = await _userReadOnlyRepository.IsEmailExists(request.Email);
            if (emailExists)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceMessageError.user_email_repeated));
            }

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidatorErrorException(errorMessage);
            }
        }
    }
}
