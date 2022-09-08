namespace ApplicantAssets.Api.Controllers;

using ApplicantAssets.Api.DTOs;
using ApplicantAssets.DataAccess.Storage.StorageInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing assets resources.
/// </summary>
[Authorize(Policy = "VIPUserPolicy")]
public class AssetsController : BaseController
{
    private readonly IApplicantMongoDbStorage storage;
    private readonly ILogger<AssetsController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssetsController"/> class.
    /// </summary>
    /// <param name="storage">An applicant storage.</param>
    /// <param name="logger">A logger.</param>
    public AssetsController(IApplicantMongoDbStorage storage, ILogger<AssetsController> logger)
    {
        this.storage = storage;
        this.logger = logger;
    }

    /// <summary>
    /// Gets all assets.
    /// </summary>
    /// <returns>A list of all available assets.</returns>
    [HttpGet("all")]
    public async Task<ActionResult> GetAssets()
    {
        this.logger.LogInformation("Getting all existing applicants..");

        return this.Ok(await this.storage.GetAllAsync());
    }

    /// <summary>
    /// Gets all assets filtered by a date and/or a country of birth.
    /// </summary>
    /// <param name="parameters">Contains necessary information for the search.</param>
    /// <returns>A list of all available assets.</returns>
    [HttpGet("some/")]
    public async Task<ActionResult> GetAssetsByDateAndCountry([FromQuery] DateAndCountryParams parameters)
    {
        this.logger.LogInformation(
            "Getting all existing applicants filtered by date: {Date} and country: {Country}...",
            parameters.Date,
            parameters.Country);

        return this.Ok(await this.storage.GetByDateAndCountryAsync(
            parameters.OlderThan,
            parameters.Date,
            parameters.Country));
    }
}
