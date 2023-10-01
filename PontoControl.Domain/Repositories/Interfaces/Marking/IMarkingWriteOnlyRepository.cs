namespace PontoControl.Domain.Repositories.Interfaces.Marking
{
    public interface IMarkingWriteOnlyRepository
    {
        Task Register(Domain.Entities.Marking marking);
    }
}
