using FluentValidation;
using PontoControl.Comunication.Requests;
using PontoControl.Exceptions;

namespace PontoControl.Application.UseCases.User.RegisterCollaborator
{
    public class RegisterCollaboratorValidator : AbstractValidator<RegisterCollaboratorRequest>
    {
        public RegisterCollaboratorValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(ResourceMessageError.user_firstname_empty);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(ResourceMessageError.user_lastname_empty);
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceMessageError.user_email_empty);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ResourceMessageError.user_email_empty);

            When(x => !String.IsNullOrWhiteSpace(x.Password), () =>
            {
                RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessageError.user_password_minlength);
            });
        }
    }
}
