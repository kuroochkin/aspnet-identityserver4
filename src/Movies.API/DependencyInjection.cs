using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movies.API.Data;

namespace Movies.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        
        services.AddDbContext<MoviesAPIContext>(options =>
            options.UseInMemoryDatabase("MoviesAPIContext"));
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "Movies.API", Version = "v1"});
        });

        return services;
    }
    
    public static WebApplication UseApiServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseHttpsRedirection();
        app.UseRouting();
        
        return app;
    }

    public static IHost SeedDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var moviesContext = services.GetRequiredService<MoviesAPIContext>();
        MoviesContextSeed.SeedAsync(moviesContext);

        return host;
    }
}