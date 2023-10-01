using AutoMapper;
using PontoControl.Application.Services.GetUserLogged;
using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;
using PontoControl.Domain.Entities;
using PontoControl.Domain.Repositories;
using PontoControl.Domain.Repositories.Interfaces.Marking;
using PontoControl.Exceptions;
using PontoControl.Exceptions.ExceptionsBase;

namespace PontoControl.Application.UseCases.Marking.Register
{
    public class RegisterMakingUseCase : IRegisterMakingUseCase
    {
        private readonly IMarkingWriteOnlyRepository _markingWriteOnlyRepository;
        private readonly IMarkingReadOnlyRepository _markingReadOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnityOfWork _unityOfWork;
        private readonly IUserLogged _userLogged;

        public RegisterMakingUseCase(IMarkingWriteOnlyRepository markingWriteOnlyRepository, IMapper mapper, 
                                     IUnityOfWork unityOfWork, IUserLogged userLogged, IMarkingReadOnlyRepository markingReadOnlyRepository)
        {
            _markingWriteOnlyRepository = markingWriteOnlyRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
            _userLogged = userLogged;
            _markingReadOnlyRepository = markingReadOnlyRepository;
        }

        public async Task<RegisterMarkingResponse> Execute(RegisterMarkingRequest request)
        {
            var userLogged = await _userLogged.GetUserLogged();

            var markingsOfDay = await _markingReadOnlyRepository.GetMarkingsOfDayByUserId(userLogged.Id);

            Validate(request, markingsOfDay);

            var marking = _mapper.Map<Domain.Entities.Marking>(request);
            marking.CollaboratorId = userLogged.Id;

            await _markingWriteOnlyRepository.Register(marking);
            await _unityOfWork.Commit();

            return new()
            {
                Marking = markingsOfDay
            };
        }

        private static void Validate(RegisterMarkingRequest request, List<Domain.Entities.Marking> markingsOfDay)
        {
            var validate = new RegisterMarkingValidator();
            var result = validate.Validate(request);

            if (markingsOfDay.Count == 4)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure("marcacoesExcedidas", ResourceMessageError.exceeded_markings));
            }

            if (!result.IsValid)
            {
                var errorMessage = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidatorErrorException(errorMessage);
            }
        }
    }
}
