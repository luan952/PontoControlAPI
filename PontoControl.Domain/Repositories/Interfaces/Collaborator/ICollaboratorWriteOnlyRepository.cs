namespace PontoControl.Domain.Repositories.Interfaces.Collaborator
{
    public interface ICollaboratorWriteOnlyRepository
    {
        Task InsertCollaborator(Entities.Collaborator collaborator);
    }
}
