using PontoControl.Comunication.Responses;

namespace PontoControl.Application.UseCases.Marking.GetOfDay
{
    public interface IGetMarkingOfDayUseCase
    {
        Task<MarkingOfDayResponse> Execute();
    }
}
