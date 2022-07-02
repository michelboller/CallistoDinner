using CallistoDinner.Application.Common.Interfaces.Authentication;
using CallistoDinner.Application.Common.Interfaces.Persistence;
using CallistoDinner.Application.Common.Services;
using CallistoDinner.Infrastructure.Authentication;
using CallistoDinner.Infrastructure.Persistence;
using CallistoDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CallistoDinner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
