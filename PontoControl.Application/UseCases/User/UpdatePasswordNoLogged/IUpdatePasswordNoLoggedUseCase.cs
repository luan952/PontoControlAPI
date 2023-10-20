using PontoControl.Comunication.Requests;

namespace PontoControl.Application.UseCases.User.UpdatePasswordNoLogged
{
    public interface IUpdatePasswordNoLoggedUseCase
    {
        Task Execute(UpdatePasswordNoLoggedRequest request);
    }
}
