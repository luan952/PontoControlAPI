using FluentValidation;
using PontoControl.Comunication.Requests;
using PontoControl.Exceptions;

namespace PontoControl.Application.UseCases.User.UpdatePassword
{
    public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordRequest>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage(ResourceMessageError.user_password_empty);
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage(ResourceMessageError.user_password_new_empty);

            When(x => !String.IsNullOrWhiteSpace(x.Password), () =>
            {
                RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessageError.user_password_minlength);
            });

            When(x => !String.IsNullOrWhiteSpace(x.NewPassword), () =>
            {
                RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessageError.user_password_minlength);
            });
        }
    }
}
