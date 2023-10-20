using FluentValidation;
using PontoControl.Application.UseCases.User.UpdatePassword;
using PontoControl.Comunication.Requests;
using PontoControl.Exceptions;

namespace PontoControl.Application.UseCases.User.UpdatePasswordNoLogged
{
    public class UpdatePasswordNoLoggedValidator : AbstractValidator<UpdatePasswordNoLoggedRequest>
    {
        public UpdatePasswordNoLoggedValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceMessageError.user_email_empty);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ResourceMessageError.user_password_empty);

            When(x => !String.IsNullOrWhiteSpace(x.Password), () =>
            {
                RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessageError.user_password_minlength);
            });
        }
    }
}
