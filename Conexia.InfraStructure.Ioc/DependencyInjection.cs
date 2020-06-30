using Conexia.Domain.Handlers;
using Conexia.Domain.Queries;
using Conexia.Domain.Repositories;
using Conexia.Domain.Shared.Facades;
using Conexia.InfraStructure.AntiCorruption.SendGrid;
using Conexia.InfraStructure.AntiCorruption.Services.OpenWeatherMap;
using Conexia.InfraStructure.AntiCorruption.Services.Spotify;
using Conexia.InfraStructure.CrossCutting.Security;
using Conexia.InfraStructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Conexia.InfraStructure.Ioc
{
    public static class DependencyInjection
    {
        public static void DependencyInjectionServices(ref IServiceCollection services)
        {
            services.AddTransient<IEncryptionFacade, Encryptions>();
            services.AddTransient<ITemperatureFacade, TemperatureFacade>();
            services.AddTransient<ISpotifyFacade, SpotifyFacade>();
            services.AddTransient<IUserQueries, UserQueries>();
            services.AddTransient<IEmailFacade, SendGridFacade>();
        }

        public static void DependencyInjectionRepository(ref IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static void DependencyInjectionHandler(ref IServiceCollection services)
        {
            services.AddScoped<UserHandler, UserHandler>();
        }
    }
}
