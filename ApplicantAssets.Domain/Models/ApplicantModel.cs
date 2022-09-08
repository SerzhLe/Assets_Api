namespace ApplicantAssets.Domain.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

/// <summary>
/// A model for applicant entity.
/// </summary>
public class ApplicantModel
{
    /// <summary>
    /// Gets or sets the id of applicant.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the given name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the surname.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date of birth.
    /// </summary>
    [BsonElement("dob")]
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Gets or sets the country of birth.
    /// </summary>
    public string CountryOfBirth { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of cars owned.
    /// </summary>
    public ICollection<CarModel>? Cars { get; set; }

    /// <summary>
    /// Gets or sets the collection of houses owned.
    /// </summary>
    public ICollection<HouseModel>? Houses { get; set; }
}
