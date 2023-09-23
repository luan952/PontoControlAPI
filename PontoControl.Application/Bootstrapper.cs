﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PontoControl.Application.Services.Cryptography;
using PontoControl.Application.Services.GetUserLogged;
using PontoControl.Application.Services.Token;
using PontoControl.Application.UseCases.User.RegisterCollaborator;

namespace PontoControl.Application
{
    public static class Bootstrapper
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddUseCases(services);
            AddPasswordEncryptor(services);
            AddTokenJWT(services, configuration);
            AddUserLogged(services);
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterCollaboratorUseCase, RegisterCollaboratorUseCase>();
        }

        private static void AddPasswordEncryptor(this IServiceCollection services)
        {
            services.AddScoped(option => new PasswordEncryptor());
        }

        private static void AddUserLogged(this IServiceCollection services)
        {
            services.AddScoped<IUserLogged, UserLogged>();
        }

        private static void AddTokenJWT(IServiceCollection services, IConfiguration configuration)
        {
            var timeLifeToken = configuration.GetRequiredSection("Configuration:TimeLifeTokenInMinutes");
            var key = configuration.GetRequiredSection("Configuration:TokenKey");

            services.AddScoped(option => new TokenController(int.Parse(timeLifeToken.Value), key.Value));
        }
    }
}
