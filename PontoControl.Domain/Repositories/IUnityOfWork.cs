namespace PontoControl.Domain.Repositories
{
    public interface IUnityOfWork
    {
        Task Commit();
    }
}
