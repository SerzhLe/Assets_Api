namespace ApplicantAssets.Domain.Models;

using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

/// <summary>
/// Application role collection.
/// </summary>
[CollectionName("roles")]
public class AppRoleModel : MongoIdentityRole<Guid>
{
}
