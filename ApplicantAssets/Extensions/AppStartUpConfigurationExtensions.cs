namespace ApplicantAssets.Api.Extensions;

using System.Text;
using ApplicantAssets.Api.Configuration;
using ApplicantAssets.Api.Middleware;
using ApplicantAssets.Api.Services;
using ApplicantAssets.DataAccess.Storage.EntityStorages;
using ApplicantAssets.DataAccess.Storage.StorageInterfaces;
using ApplicantAssets.Domain.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// Class that contains extensions methods for configuring the app start up.
/// </summary>
public static class AppStartUpConfigurationExtensions
{
    /// <summary>
    /// Adds all the necessary services for app.
    /// </summary>
    /// <param name="services">A collection that contains all app services.</param>
    /// <param name="config">Represents key/value application pair properties.</param>
    /// <returns>A service collection.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        // Add services to the container.
        _ = services.Configure<MongoDbConfig>(config.GetSection(nameof(MongoDbConfig)));

        var mongoDbConfig = config.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

        _ = services
            .AddIdentityCore<AppUserModel>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<AppRoleModel>()
            .AddRoleManager<RoleManager<AppRoleModel>>()
            .AddSignInManager<SignInManager<AppUserModel>>()
            .AddRoleValidator<RoleValidator<AppRoleModel>>()
            .AddMongoDbStores<AppUserModel, AppRoleModel, Guid>(
                mongoDbConfig.Connection,
                mongoDbConfig.Database);

        _ = services.AddScoped<IApplicantMongoDbStorage, ApplicantMongoDbStorage>();

        _ = services.AddScoped<ITokenService, TokenService>();

        _ = services.AddControllers();

        var tokenConfig = config.GetSection("TokenConfig");

        _ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
            });

        _ = services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
            options.AddPolicy("VIPUserPolicy", policy => policy.RequireRole("VIPUser", "Admin"));
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        _ = services.AddEndpointsApiExplorer();
        _ = services.AddSwaggerGen();

        return services;
    }

    /// <summary>
    /// Adds all necessary middleware for app.
    /// </summary>
    /// <param name="app">A web application used for configuring Http pipeline.</param>
    /// <param name="env">A web host environment an app is running in.</param>
    /// <returns>A reference to web application.</returns>
    public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI();
        }

        _ = app.UseMiddleware<ExceptionMiddleware>();

        _ = app.UseHttpsRedirection();

        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        return app;
    }
}
