namespace ApplicantAssets.Api.DTOs;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// DTO for login.
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Gets or sets username.
    /// </summary>
    [Required]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets user's password.
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}
