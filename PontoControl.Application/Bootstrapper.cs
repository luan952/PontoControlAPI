using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PontoControl.Application.UseCases.User.RegisterCollaborator;

namespace PontoControl.Application
{
    public static class Bootstrapper
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddUseCases(services);
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterCollaboratorUseCase, RegisterCollaboratorUseCase>();
        }
    }
}
