using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;

namespace PontoControl.Application.UseCases.User.RegisterCollaborator
{
    public interface IRegisterCollaboratorUseCase
    {
        Task<RegisterCollaboratorResponse> Execute(RegisterCollaboratorRequest request);
    }
}
