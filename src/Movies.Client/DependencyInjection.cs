using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Net.Http.Headers;
using Movies.Client.ApiServices;
using Movies.Client.HttpHandlers;

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
                options.ResponseType = "code id_token";
                
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("movieAPI");

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
            });

        services.AddTransient<AuthenticationDelegatingHandler>();

        services.AddHttpClient("MovieAPIClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:5001/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

        services.AddHttpClient("IDPClient", client =>
        {
            client.BaseAddress = new Uri("https://localhost:5005/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });

        services.AddHttpContextAccessor();

        // services.AddSingleton(new ClientCredentialsTokenRequest
        // {
        //     Address = "https://localhost:5005/connect/token",
        //     ClientId = "movieClient",
        //     ClientSecret = "secret",
        //     Scope = "movieAPI"
        // });

        return services;
    }
}