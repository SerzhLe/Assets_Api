namespace ApplicantAssets.Api.DTOs;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Authenticated user dto.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Gets or sets username.
    /// </summary>
    [Required]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets authenticating token.
    /// </summary>
    [Required]
    public string Token { get; set; } = string.Empty;
}
