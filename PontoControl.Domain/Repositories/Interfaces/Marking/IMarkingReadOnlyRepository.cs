namespace PontoControl.Domain.Repositories.Interfaces.Marking
{
    public interface IMarkingReadOnlyRepository
    {
        Task<List<Domain.Entities.Marking>> GetMarkingsOfDayByUserId(Guid userId);
        Task<List<Domain.Entities.Marking>> GetMarkingsByUser(Guid userId);
    }
}
