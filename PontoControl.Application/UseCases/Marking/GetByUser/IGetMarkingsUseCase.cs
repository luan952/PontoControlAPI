using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;

namespace PontoControl.Application.UseCases.Marking.GetByUser
{
    public interface IGetMarkingsUseCase
    {
        Task<GetMarkingResponse> Execute(GetMarkingByUserRequest request);
    }
}
