using Movies.Client.ApiServices;

namespace Movies.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddMvcLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IMovieApiService, MovieApiService>();

        return services;
    }
}