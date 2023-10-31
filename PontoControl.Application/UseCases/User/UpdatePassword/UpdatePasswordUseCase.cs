using PontoControl.Application.Services.Cryptography;
using PontoControl.Application.Services.GetUserLogged;
using PontoControl.Comunication.Requests;
using PontoControl.Domain.Entities;
using PontoControl.Domain.Repositories;
using PontoControl.Domain.Repositories.Interfaces.User;
using PontoControl.Exceptions;
using PontoControl.Exceptions.ExceptionsBase;

namespace PontoControl.Application.UseCases.User.UpdatePassword
{
    public class UpdatePasswordUseCase : IUpdatePasswordUseCase
    {
        private readonly IUserLogged _userLogged;
        private readonly PasswordEncryptor _passwordEncryptor;
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;

        public UpdatePasswordUseCase(IUserLogged userLogged, PasswordEncryptor passwordEncryptor,
                                     IUserUpdateOnlyRepository userUpdateOnlyRepository, IUnityOfWork unityOfWork)
        {
            _userLogged = userLogged;
            _passwordEncryptor = passwordEncryptor;
            _userUpdateOnlyRepository = userUpdateOnlyRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task Execute(UpdatePasswordRequest request)
        {
            var userLogged = await _userLogged.GetUserLogged();
            var updateUser = await _userUpdateOnlyRepository.GetUserById(userLogged.Id);

            Validate(updateUser, request);

            if ((bool)!userLogged.IsFirstLogin)
                updateUser.IsFirstLogin = true;

            updateUser.Password = _passwordEncryptor.Encrypt(request.NewPassword);

            _userUpdateOnlyRepository.Update(updateUser);
            await _unityOfWork.Commit();
        }

        private void Validate(Domain.Entities.User user, UpdatePasswordRequest request)
        {
            var validator = new UpdatePasswordValidator();
            var result = validator.Validate(request);

            var currentPassword = _passwordEncryptor.Encrypt(request.Password);

            if (!currentPassword.Equals(user.Password))
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("senhaAtual", ResourceMessageError.current_password_invalid));
            }

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidatorErrorException(errorMessage);
            }
        }
    }
}
