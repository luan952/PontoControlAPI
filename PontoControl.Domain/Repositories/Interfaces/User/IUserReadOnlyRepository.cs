namespace PontoControl.Domain.Repositories.Interfaces.User
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> IsEmailExists(string email);
    }
}
