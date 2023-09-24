using FluentValidation;
using PontoControl.Comunication.Requests;
using PontoControl.Exceptions;
using System.Text.RegularExpressions;

namespace PontoControl.Application.UseCases.User.RegisterCollaborator
{
    public class RegisterCollaboratorValidator : AbstractValidator<RegisterCollaboratorRequest>
    {
        public RegisterCollaboratorValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage(ResourceMessageError.user_firstname_empty);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(ResourceMessageError.user_lastname_empty);
            RuleFor(x => x.Email).NotEmpty().WithMessage(ResourceMessageError.user_email_empty);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ResourceMessageError.user_password_empty);
            RuleFor(x => x.Position).NotEmpty().WithMessage(ResourceMessageError.user_position_empty);

            When(x => !String.IsNullOrWhiteSpace(x.Password), () =>
            {
                RuleFor(u => u.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessageError.user_password_minlength);
            });

            When(x => !String.IsNullOrWhiteSpace(x.Document), () =>
            {
                RuleFor(u => u.Document).Custom((document, context) =>
                {
                    string pattern = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$";
                    var isValid = Regex.IsMatch(document, pattern);

                    if (!isValid)
                        context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(document), ResourceMessageError.document_invalid));
                });
            });
        }
    }
}
