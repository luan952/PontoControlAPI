using Microsoft.EntityFrameworkCore;
using PontoControl.Domain.Entities;

namespace PontoControl.Infra.RepositoryAccess
{
    public class PontoControlContext : DbContext
    {
        public PontoControlContext(DbContextOptions<PontoControlContext> options) : base(options) { }

        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Marking> Markings { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PontoControlContext).Assembly);
        }
    }
}
