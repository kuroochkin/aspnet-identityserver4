using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Movies.API.Data;

namespace Movies.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo {Title = "Movies.API", Version = "v1"});
        });
        
        services.AddDbContext<MoviesAPIContext>(options =>
            options.UseInMemoryDatabase("MoviesAPIContext"));

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5005";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("ClientIdPolicy", policy => 
                policy.RequireClaim("client_id", "movieClient", "movies_mvc_client"));
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

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
        
        var host = app.Services.GetService<IHost>();
        host?.SeedDatabase();
        
        return app;
    }

    private static IHost SeedDatabase(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var moviesContext = services.GetRequiredService<MoviesAPIContext>();
        MoviesContextSeed.SeedAsync(moviesContext);

        return host;
    }
}