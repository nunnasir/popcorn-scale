using Microsoft.Extensions.DependencyInjection;
using PopcornScale.Application.Repositories;

namespace PopcornScale.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IMovieRepository, MovieRepository>();

        return services;
    }
}
