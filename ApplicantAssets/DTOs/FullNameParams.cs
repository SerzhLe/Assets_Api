namespace ApplicantAssets.Api.DTOs;

/// <summary>
/// Full name DTO.
/// </summary>
public class FullNameParams
{
    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;
}
