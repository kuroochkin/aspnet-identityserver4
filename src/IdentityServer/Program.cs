using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryClients(Config.Clients)
    .AddInMemoryApiScopes(Config.ApiScopes)
    // .AddInMemoryApiResources(Config.ApiResources)
    // .AddInMemoryIdentityResources(Config.IdentityResources)
    // .AddTestUsers(Config.TestUsers)
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseIdentityServer();

app.Run();