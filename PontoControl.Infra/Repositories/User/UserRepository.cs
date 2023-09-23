using Microsoft.EntityFrameworkCore;
using PontoControl.Domain.Entities;
using PontoControl.Domain.Repositories.Interfaces.User;
using PontoControl.Infra.RepositoryAccess;

namespace PontoControl.Infra.Repositories.User
{
    public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly PontoControlContext _context;

        public UserRepository(PontoControlContext context)
        {
            _context = context;
        }

        public async Task InsertCollaborator(Collaborator collaborator)
        {
            await _context.Collaborators.AddAsync(collaborator);
        }

        public async Task<bool> IsEmailExists(string email)
        {
            return await _context.Collaborators.AnyAsync(c => c.Email.Equals(email));
        }
    }
}
