using CallistoDinner.Api.Common.Mapping;
using CallistoDinner.Api.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CallistoDinner.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddSingleton<ProblemDetailsFactory, CallistoDinnerProblemDetailsFactory>();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers();
            services.AddMappings();

            return services;
        }
    }
}
