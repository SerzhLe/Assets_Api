namespace ApplicantAssets.Api.DTOs;

/// <summary>
/// DTO for storing date and country parameters.
/// </summary>
public class DateAndCountryParams
{
    /// <summary>
    /// Gets or sets the date of birth.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Gets or sets the country of birth.
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to find applicants older or younger than the specified date of birth.
    /// </summary>
    public bool OlderThan { get; set; }
}
