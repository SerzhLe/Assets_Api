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
    /// Gets all applicants including assets.
    /// </summary>
    /// <returns>A list of all available assets.</returns>
    [HttpGet("all")]
    public async Task<ActionResult> GetApplicants()
    {
        this.logger.LogInformation("Getting all existing applicants..");

        return this.Ok(await this.storage.GetAllAsync());
    }

    /// <summary>
    /// Gets all applicants filtered by a date and a country of birth including assets.
    /// </summary>
    /// <param name="parameters">Contains necessary information for the search.</param>
    /// <returns>A list of all available assets.</returns>
    [HttpGet("dateandcountry")]
    public async Task<ActionResult> GetApplicantsByDateAndCountry([FromQuery] DateAndCountryParams parameters)
    {
        if (parameters is null)
        {
            return this.BadRequest("Query parameters are not specified");
        }

        this.logger.LogInformation(
            "Getting all existing applicants filtered by date: {Date} and country: {Country}...",
            parameters.Date,
            parameters.Country);

        var result = await this.storage.GetByDateAndCountryAsync(
            parameters.OlderThan,
            parameters.Date,
            parameters.Country);

        if (!result.Any())
        {
            return this.NotFound("There are no applicants match the specified condition.");
        }

        return this.Ok(result);
    }

    /// <summary>
    /// Gets an applicant by their full name with assets.
    /// </summary>
    /// <param name="parameters">Contains necessary information for the search.</param>
    /// <returns>An applicant with corresponding full name.</returns>
    [HttpGet("fullname")]
    public async Task<ActionResult> GetApplicantByFullName([FromQuery] FullNameParams parameters)
    {
        if (parameters is null)
        {
            return this.BadRequest("Query parameters are not specified");
        }

        if (string.IsNullOrWhiteSpace(parameters.FirstName) || string.IsNullOrWhiteSpace(parameters.LastName))
        {
            return this.BadRequest("First name and/or last name cannot be empty.");
        }

        this.logger.LogInformation(
            "Getting an applicant by their full name: {FirstName}, {LastName}",
            parameters.FirstName,
            parameters.LastName);

        var result = await this.storage.GetByFullName(parameters.FirstName, parameters.LastName);

        if (result is null)
        {
            return this.NotFound("There is no applicant with the specified full name.");
        }

        return this.Ok(result);
    }
}
