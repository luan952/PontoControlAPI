using PontoControl.Application.Services.Cryptography;
using PontoControl.Domain.Entities;
using PontoControl.Domain.Repositories;
using PontoControl.Infra.RepositoryAccess;

namespace PontoControl.Application.Services.DataSeeder
{
    public class DataSeeder
    {
        private readonly PontoControlContext _context;
        private readonly PasswordEncryptor _encryptor;
        private readonly IUnityOfWork _unityOfWork;
        public DataSeeder(PontoControlContext context, PasswordEncryptor encryptor, IUnityOfWork unityOfWork)
        {
            _context = context;
            _encryptor = encryptor;
            _unityOfWork = unityOfWork;
        }

        public void SeedData()
        {
            if (!_context.Admins.Any())
            {
                var id = Guid.NewGuid();

                var admin = new Admin
                {
                    Id = id,
                    Email = "admin@admin.com",
                    FirstName = "Admin",
                    Password = _encryptor.Encrypt("senha123456"),
                    IsFirstLogin = true,
                    TypeUser = Domain.Enum.UserType.ADMIN
                };

                _context.Admins.Add(admin);

                _context.SaveChanges();
            }
        }
    }
}
