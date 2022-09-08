namespace ApplicantAssets.DataAccess.Storage.EntityStorages;

using ApplicantAssets.DataAccess.Storage.StorageInterfaces;
using ApplicantAssets.Domain.Configuration;
using ApplicantAssets.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

/// <summary>
/// The mongo db applicant storage API.
/// </summary>
public class ApplicantMongoDbStorage : BaseMongoDbStorage<ApplicantModel, Guid>, IApplicantMongoDbStorage
{
    private const string ApplicantCollection = "applicants";

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicantMongoDbStorage"/> class.
    /// </summary>
    /// <param name="configuration"> <inheritdoc/></param>
    public ApplicantMongoDbStorage(IOptions<MongoDbConfig> configuration)
        : base(configuration, ApplicantCollection)
    {
    }

    /// <inheritdoc/>
    public async Task<List<ApplicantModel>> GetByDateAndCountryAsync(bool olderThan, DateTime dob, string? country)
    {
        var filter = olderThan

            // true if applicant's dob is greater than parameter
            ? Builders<ApplicantModel>.Filter.Lt(ap => ap.DateOfBirth, dob)

            // you compare value with field, true if value is greater than the field
            : Builders<ApplicantModel>.Filter.Gt(ap => ap.DateOfBirth, dob);

        if (country is not null)
        {
            var filter2 = Builders<ApplicantModel>.Filter.Eq(ap => ap.CountryOfBirth, country);

            filter = Builders<ApplicantModel>.Filter.And(filter, filter2);
        }

        var sorter = Builders<ApplicantModel>.Sort.Descending(ap => ap.DateOfBirth);

        var findOptions = new FindOptions<ApplicantModel> { Sort = sorter };

        return await (await this.Collection.FindAsync(filter, findOptions)).ToListAsync();
    }
}
