namespace ApplicantAssets.Api.Services;

using ApplicantAssets.Domain.Models;

/// <summary>
/// Service for generating authenticating token.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates JWT token for specified user.
    /// </summary>
    /// <param name="user">A user to generate token for.</param>
    /// <returns>Encrypted string representation of the token.</returns>
    Task<string> CreateTokenAsync(AppUserModel user);
}
