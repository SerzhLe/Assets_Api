namespace ApplicantAssets.DataAccess.Storage.StorageInterfaces;

using ApplicantAssets.Domain.Models;

/// <summary>
/// An interface for mongo db applicant storage API.
/// </summary>
public interface IApplicantMongoDbStorage : IBaseDbStorage<ApplicantModel, Guid>
{
    /// <summary>
    /// Gets the applicant based on the condition.
    /// </summary>
    /// <param name="olderThan">Whether to search for the applicants older or younger that the date of birth.</param>
    /// <param name="dob">A date of birth to compare.</param>
    /// <param name="country">A country of birth.</param>
    /// <returns>A list of filtered applicants.</returns>
    Task<List<ApplicantModel>> GetByDateAndCountryAsync(bool olderThan, DateTime dob, string? country);
}
