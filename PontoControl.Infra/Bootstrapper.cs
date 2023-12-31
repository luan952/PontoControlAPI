﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PontoControl.Domain.Repositories;
using PontoControl.Domain.Repositories.Interfaces.Collaborator;
using PontoControl.Domain.Repositories.Interfaces.Marking;
using PontoControl.Domain.Repositories.Interfaces.User;
using PontoControl.Infra.Repositories.Collaborator;
using PontoControl.Infra.Repositories.Marking;
using PontoControl.Infra.Repositories.User;
using PontoControl.Infra.RepositoryAccess;

namespace PontoControl.Infra
{
    public static class Bootstrapper
    {
        public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            AddContext(services, configuration);
            AddRepositories(services);
            AddUnityOfWork(services);
        }

        public static void AddContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PontoControlContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void AddRepositories(IServiceCollection services)
        {
            services
                .AddScoped<IUserWriteOnlyRepository, UserRepository>()
                .AddScoped<IUserReadOnlyRepository, UserRepository>()
                .AddScoped<IUserUpdateOnlyRepository, UserRepository>()
                .AddScoped<ICollaboratorWriteOnlyRepository, CollaboratorRepository>()
                .AddScoped<ICollaboratorReadOnlyRepository, CollaboratorRepository>()
                .AddScoped<IMarkingReadOnlyRepository, MarkingRepository>()
                .AddScoped<IMarkingWriteOnlyRepository, MarkingRepository>();
        }

        public static void AddUnityOfWork(IServiceCollection services)
        {
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }
    }
}
