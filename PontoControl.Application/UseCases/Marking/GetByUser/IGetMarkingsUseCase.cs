using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;

namespace PontoControl.Application.UseCases.Marking.GetByUser
{
    public interface IGetMarkingsUseCase
    {
        Task<List<GetMarkingResponse>> Execute(GetMarkingByUserRequest request);
    }
}
