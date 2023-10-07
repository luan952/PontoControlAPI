using Microsoft.EntityFrameworkCore;
using PontoControl.Domain.Repositories.Interfaces.Marking;
using PontoControl.Infra.RepositoryAccess;

namespace PontoControl.Infra.Repositories.Marking
{
    public class MarkingRepository : IMarkingReadOnlyRepository, IMarkingWriteOnlyRepository
    {
        private readonly PontoControlContext _context;

        public MarkingRepository(PontoControlContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Marking>> GetMarkingsByUser(Guid userId)
        {
            return await _context.Markings.
                                    AsNoTracking().
                                    Where(m => m.CollaboratorId == userId).
                                    OrderBy(m => m.Hour).ToListAsync();
        }

        public async Task<List<Domain.Entities.Marking>> GetMarkingsOfDayByUserId(Guid userId)
        {
            return await _context.Markings.
                                    AsNoTracking().
                                    Where(m => m.CollaboratorId == userId && m.Hour.Date == DateTime.Now.Date).
                                    OrderBy(m => m.Hour).ToListAsync();
        }

        public async Task Register(Domain.Entities.Marking marking)
        {
            await _context.Markings.AddAsync(marking);
        }
    }
}
