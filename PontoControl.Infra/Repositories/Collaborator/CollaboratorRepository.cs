using Microsoft.EntityFrameworkCore;
using PontoControl.Domain.Repositories.Interfaces.Collaborator;
using PontoControl.Infra.RepositoryAccess;

namespace PontoControl.Infra.Repositories.Collaborator
{
    public class CollaboratorRepository : ICollaboratorWriteOnlyRepository, ICollaboratorReadOnlyRepository
    {
        private readonly PontoControlContext _context;

        public CollaboratorRepository(PontoControlContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.Collaborator> GetCollaboratorById(Guid userId)
        {
            return await _context.Collaborators
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == userId);
        }

        public async Task InsertCollaborator(Domain.Entities.Collaborator collaborator)
        {
            await _context.Collaborators.AddAsync(collaborator);
        }
    }
}
