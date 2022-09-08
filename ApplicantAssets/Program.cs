namespace ApplicantAssets.Api;
using ApplicantAssets.Api.Extensions;

/// <summary>
/// Basic class for program entrypoint.
/// </summary>
public class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// <param name="args">An arguments.</param>
    /// <returns>A <see cref="Task"/> that represents an asynchronous operation.</returns>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // add all necessary services
        _ = builder.Services.AddServices(builder.Configuration);

        var app = builder.Build();

        // use all necessary middleware
        _ = app.UseMiddlewares(app.Environment);

        _ = app.MapControllers();

        _ = await app.SeedDataBaseAsync();

        await app.RunAsync();
    }
}
