namespace ApplicantAssets.DataAccess.Seed;

using System.Text.Json;
using ApplicantAssets.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

/// <summary>
/// Class for seeding db with random data from json file.
/// </summary>
public static class Seed
{
    /// <summary>
    /// Seeds db with data read from json file.
    /// </summary>
    /// <typeparam name="T">An entity type to seed.</typeparam>
    /// <param name="filePath">A path to json file with random data.</param>
    /// <param name="collectionName">A name of db collection to seed read data.</param>
    /// <param name="userManager">An api provider to work with users.</param>
    /// <param name="roleManager">An api provider to work with roles.</param>
    /// <param name="config">A configuration.</param>
    /// <returns>A <see cref="Task"/> that represents an asynchronous operation.</returns>
    public static async Task SeedDataAsync<T>(
        string filePath,
        string collectionName,
        UserManager<AppUserModel> userManager,
        RoleManager<AppRoleModel> roleManager,
        IConfiguration config)
    {
        var client = new MongoClient("mongodb://localhost:27017");

        var db = client.GetDatabase("assets");

        var collection = db.GetCollection<T>(collectionName);

        var count = await collection.EstimatedDocumentCountAsync();

        if (count <= 0)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var data = await JsonSerializer.DeserializeAsync<IEnumerable<T>>(stream, options);

                await collection.InsertManyAsync(data);
            }
        }

        if (!roleManager.Roles.Any())
        {
            _ = await roleManager.CreateAsync(new AppRoleModel { Name = "Admin" });
            _ = await roleManager.CreateAsync(new AppRoleModel { Name = "VIPUser" });
            _ = await roleManager.CreateAsync(new AppRoleModel { Name = "User" });
        }

        if (!userManager.Users.Any())
        {
            var admin = new AppUserModel { UserName = "admin", SecurityStamp = Guid.NewGuid().ToString() };
            var vipUser = new AppUserModel { UserName = "vip_user", SecurityStamp = Guid.NewGuid().ToString() };
            var user = new AppUserModel { UserName = "simple_user", SecurityStamp = Guid.NewGuid().ToString() };

            _ = await userManager.CreateAsync(admin, config["AdminPassword"]);
            _ = await userManager.CreateAsync(vipUser, config["VIPUserPassword"]);
            _ = await userManager.CreateAsync(user, config["UserPassword"]);

            _ = await userManager.AddToRoleAsync(admin, "Admin");
            _ = await userManager.AddToRoleAsync(vipUser, "VIPUser");
            _ = await userManager.AddToRoleAsync(user, "User");
        }
    }
}
