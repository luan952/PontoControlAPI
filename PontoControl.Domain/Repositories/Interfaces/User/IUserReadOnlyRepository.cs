using PontoControl.Domain.Entities;

namespace PontoControl.Domain.Repositories.Interfaces.User
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> IsEmailExists(string email);
        Task<Domain.Entities.User> GetUserByEmail(string email);
        Task<Domain.Entities.User> Login(string email, string password);
    }
}
