using PontoControl.Application.Services.Cryptography;
using PontoControl.Application.UseCases.User.UpdatePassword;
using PontoControl.Comunication.Requests;
using PontoControl.Exceptions.ExceptionsBase;
using PontoControl.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PontoControl.Application.Services.GetUserLogged;
using PontoControl.Domain.Repositories.Interfaces.User;
using PontoControl.Domain.Repositories;

namespace PontoControl.Application.UseCases.User.UpdatePasswordNoLogged
{
    public class UpdatePasswordNoLoggedUseCase : IUpdatePasswordNoLoggedUseCase
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly PasswordEncryptor _passwordEncryptor;
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;
        private readonly IUnityOfWork _unityOfWork;

        public UpdatePasswordNoLoggedUseCase(IUserReadOnlyRepository userReadOnlyRepository, PasswordEncryptor passwordEncryptor,
                                     IUserUpdateOnlyRepository userUpdateOnlyRepository, IUnityOfWork unityOfWork)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _passwordEncryptor = passwordEncryptor;
            _userUpdateOnlyRepository = userUpdateOnlyRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task Execute(UpdatePasswordNoLoggedRequest request)
        {
            var user = await _userReadOnlyRepository.GetUserByEmail(request.Email);

            if (user is null)
                throw new ValidatorErrorException(new List<string>() {ResourceMessageError.user_email_not_exists});

            var updateUser = await _userUpdateOnlyRepository.GetUserById(user.Id);

            Validate(updateUser, request);

            if ((bool)!updateUser.IsFirstLogin)
                updateUser.IsFirstLogin = true;

            updateUser.Password = _passwordEncryptor.Encrypt(request.NewPassword);
            _userUpdateOnlyRepository.Update(updateUser);
            await _unityOfWork.Commit();
        }

        private void Validate(Domain.Entities.User user, UpdatePasswordNoLoggedRequest request)
        {
            var validator = new UpdatePasswordNoLoggedValidator();
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
