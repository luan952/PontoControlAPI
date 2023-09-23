using PontoControl.Comunication.Requests;
using PontoControl.Comunication.Responses;

namespace PontoControl.Application.UseCases.Login.DoLogin
{
    public interface ILoginUseCase
    {
        Task<ResponseLogin> Execute(RequestLogin request);
    }
}
