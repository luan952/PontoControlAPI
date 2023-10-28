using PontoControl.Application.Services.GetUserLogged;
using PontoControl.Comunication.Responses;
using PontoControl.Domain.Repositories.Interfaces.Marking;

namespace PontoControl.Application.UseCases.Marking.GetOfDay
{
    public class GetMarkingOfDayUseCase : IGetMarkingOfDayUseCase
    {
        private readonly IUserLogged _userLogged;
        private readonly IMarkingReadOnlyRepository _markingReadOnlyRepository;

        public GetMarkingOfDayUseCase(IUserLogged userLogged, IMarkingReadOnlyRepository markingReadOnlyRepository)
        {
            _userLogged = userLogged;
            _markingReadOnlyRepository = markingReadOnlyRepository;
        }

        public async Task<MarkingOfDayResponse> Execute()
        {
            var user = await _userLogged.GetUserLogged();

            var markings = await _markingReadOnlyRepository.GetMarkingsOfDayByUserId(user.Id);

            if (markings is null)
                return new();

            return new()
            {
                MarkingsOfDay = new GetMarkingResponse() { Marking = markings.Select(m => new MarkingResponse 
                    {
                        Hour = m.Hour,
                        Address = m.Address,
                    }).ToList()
                }
            };
        }
    }
}
