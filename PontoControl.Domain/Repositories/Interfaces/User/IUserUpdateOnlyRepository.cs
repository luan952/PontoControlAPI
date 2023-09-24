namespace PontoControl.Domain.Repositories.Interfaces.User
{
    public interface IUserUpdateOnlyRepository
    {
        Task<Domain.Entities.User> GetUserById(Guid id);
        void Update(Domain.Entities.User user);
    }
}
