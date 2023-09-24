using PontoControl.Comunication.Requests;

namespace PontoControl.Application.UseCases.User.UpdatePassword
{
    public interface IUpdatePasswordUseCase
    {
        Task Execute(UpdatePasswordRequest request);
    }
}
