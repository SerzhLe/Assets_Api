namespace ApplicantAssets.Api.Extensions;

using ApplicantAssets.DataAccess.Seed;
using ApplicantAssets.Domain.Models;

using Microsoft.AspNetCore.Identity;

/// <summary>
/// Contains extension method for seeding database.
/// </summary>
public static class SeedExtensions
{
    /// <summary>
    /// Extension method for seeding database.
    /// </summary>
    /// <param name="app">A web application.</param>
    /// <returns>Web application instance.</returns>
    public static async Task<WebApplication> SeedDataBaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;

        try
        {
            var userManager = services.GetRequiredService<UserManager<AppUserModel>>();
            var roleManager = services.GetRequiredService<RoleManager<AppRoleModel>>();

            await Seed.SeedDataAsync<ApplicantModel>(
                "..\\ApplicantAssets.DataAccess\\Seed\\Applicants.json",
                "applicants",
                userManager,
                roleManager,
                app.Configuration);
        }
        catch (Exception ex)
        {
            var logger = app.Logger;

            logger.LogError(ex, "An error occurred while seeding database. {Message}", ex.Message);
        }

        return app;
    }
}
