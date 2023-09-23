using PontoControl.Domain.Entities;

namespace PontoControl.Application.Services.GetUserLogged
{
    public interface IUserLogged
    {
        Task<User> GetUserLogged();
    }
}
