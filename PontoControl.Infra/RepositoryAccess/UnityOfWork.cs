using PontoControl.Domain.Repositories;

namespace PontoControl.Infra.RepositoryAccess
{
    public class UnityOfWork : IUnityOfWork, IDisposable
    {
        private readonly PontoControlContext _context;
        private bool _disposed;
        public UnityOfWork(PontoControlContext context)
        {
            _context = context;
        }
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
}
