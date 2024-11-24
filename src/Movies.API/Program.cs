using Movies.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiServices(builder.Configuration);

var app = builder.Build();

app.UseApiServices();
app.MapControllers();

var host = app.Services.GetService<IHost>();
host?.SeedDatabase();

app.Run();