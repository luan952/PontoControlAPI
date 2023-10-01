using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;

namespace PontoControl.Application.UseCases.Marking.Register
{
    public interface IRegisterMakingUseCase
    {
        Task<RegisterMarkingResponse> Execute(RegisterMarkingRequest request);
    }
}
