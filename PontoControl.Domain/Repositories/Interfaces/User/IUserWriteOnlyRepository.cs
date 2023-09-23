using PontoControl.Domain.Entities;

namespace PontoControl.Domain.Repositories.Interfaces.User
{
    public interface IUserWriteOnlyRepository
    {
        Task InsertCollaborator(Collaborator collaborator);
    }
}
