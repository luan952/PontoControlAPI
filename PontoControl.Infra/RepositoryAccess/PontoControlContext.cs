using Microsoft.EntityFrameworkCore;
using PontoControl.Domain.Entities;

namespace PontoControl.Infra.RepositoryAccess
{
    public class PontoControlContext : DbContext
    {
        public PontoControlContext(DbContextOptions<PontoControlContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Marking> Markings { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Collaborator>().ToTable("Collaborators");

            modelBuilder.Entity<Collaborator>()
                .HasOne(c => c.Admin)
                .WithMany(a => a.Collaborators)
                .HasForeignKey(c => c.AdminId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PontoControlContext).Assembly);
        }
    }
}
