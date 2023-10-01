using FluentValidation;
using PontoControl.Comunication.Requests;
using PontoControl.Exceptions;

namespace PontoControl.Application.UseCases.Marking.Register
{
    public class RegisterMarkingValidator : AbstractValidator<RegisterMarkingRequest>
    {
        public RegisterMarkingValidator()
        {
            RuleFor(x => x.Address).NotEmpty().WithMessage(ResourceMessageError.marking_address_empty);
            RuleFor(x => x.Hour).NotEmpty().WithMessage(ResourceMessageError.marking_hour_empty);

        }
    }
}
