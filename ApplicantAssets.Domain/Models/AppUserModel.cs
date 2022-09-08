namespace ApplicantAssets.Domain.Models;

using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

/// <summary>
/// Application user collection.
/// </summary>
[CollectionName("users")]
public class AppUserModel : MongoIdentityUser<Guid>
{
}
