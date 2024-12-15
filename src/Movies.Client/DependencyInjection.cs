using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Movies.Client.ApiServices;

namespace Movies.Client;

public static class DependencyInjection
{
    public static IServiceCollection AddMvcLayer(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddScoped<IMovieApiService, MovieApiService>();

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://localhost:5005";

                options.ClientId = "movies_mvc_client";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                
                options.Scope.Add("openid");
                options.Scope.Add("profile");

                options.SaveTokens = true;

                options.GetClaimsFromUserInfoEndpoint = true;
            });

        return services;
    }
}