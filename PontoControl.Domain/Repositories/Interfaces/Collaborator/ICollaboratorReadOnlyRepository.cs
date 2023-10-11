namespace PontoControl.Domain.Repositories.Interfaces.Collaborator
{
    public interface ICollaboratorReadOnlyRepository
    {
        Task<Entities.Collaborator> GetCollaboratorById(Guid userId);
    }
}
