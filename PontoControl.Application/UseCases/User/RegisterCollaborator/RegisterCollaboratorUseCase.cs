using AutoMapper;
using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;
using PontoControl.Domain.Repositories.Interfaces.User;


namespace PontoControl.Application.UseCases.User.RegisterCollaborator
{
    public class RegisterCollaboratorUseCase : IRegisterCollaboratorUseCase
    {
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IMapper _mapper;

        public RegisterCollaboratorUseCase(IUserWriteOnlyRepository userWriteOnlyRepository, 
                                           IUserReadOnlyRepository userReadOnlyRepository,
                                           IMapper mapper)
        {
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _userReadOnlyRepository = userReadOnlyRepository;
            _mapper = mapper;
        }

        public async Task<RegisterCollaboratorResponse> Execute(RegisterCollaboratorRequest request)
        {
            await Validate(request);
            var user = _mapper.Map<Domain.Entities.Collaborator>(request);

            await _userWriteOnlyRepository.InsertCollaborator(user);
            throw new NotImplementedException();
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
                throw new ValidatorErrorsException(errorMessage);
            }
        }
    }
}
