using System.Security.Claims;
using IdentityModel;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer;

public static class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var alice = userManager.FindByNameAsync("alice").Result;
        
        if (alice == null)
        {
            alice = new ApplicationUser
            {
                UserName = "alice",
                Email = "AliceSmith@email.com",
                EmailConfirmed = true,
            };
                
            var result = userManager.CreateAsync(alice, "Pass123$").Result;
                
            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);
                
            result = userManager.AddClaimsAsync(alice, new[]{
                new Claim(JwtClaimTypes.Subject, "5BE86359-073C-434B-AD2D-A3932222DABB"),
                new Claim(JwtClaimTypes.Name, "Alice Smith"),
                new Claim(JwtClaimTypes.GivenName, "Alice"),
                new Claim(JwtClaimTypes.FamilyName, "Smith"),
                new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
            }).Result;
                
            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);
        }

        var bob = userManager.FindByNameAsync("bob").Result;
            
        if (bob != null) 
            return;
        
        bob = new ApplicationUser
        {
            UserName = "bob",
            Email = "BobSmith@email.com",
            EmailConfirmed = true
        };
            
        var resultBob = userManager.CreateAsync(bob, "Pass123$").Result;
            
        if (!resultBob.Succeeded)
            throw new Exception(resultBob.Errors.First().Description);
            
        resultBob = userManager.AddClaimsAsync(bob, new[]{
            new Claim(JwtClaimTypes.Subject, "5BE86359-073C-434B-AD2D-A3932222DABE"),
            new Claim(JwtClaimTypes.Name, "Bob Smith"),
            new Claim(JwtClaimTypes.GivenName, "Bob"),
            new Claim(JwtClaimTypes.FamilyName, "Smith"),
            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
            new Claim("location", "somewhere")
        }).Result;
            
        if (!resultBob.Succeeded)
            throw new Exception(resultBob.Errors.First().Description);
    }
}
