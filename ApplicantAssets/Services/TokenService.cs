namespace ApplicantAssets.Api.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApplicantAssets.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

/// <inheritdoc/>
public class TokenService : ITokenService
{
    private readonly UserManager<AppUserModel> userManager;
    private readonly IConfiguration config;
    private readonly SymmetricSecurityKey key;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenService"/> class.
    /// </summary>
    /// <param name="userManager">An api provider for managing app users.</param>
    /// <param name="config">An app configuration.</param>
    public TokenService(
        UserManager<AppUserModel> userManager,
        IConfiguration config)
    {
        this.userManager = userManager;
        this.config = config;
        this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.config["TokenKey"]));
    }

    /// <inheritdoc/>
    public async Task<string> CreateTokenAsync(AppUserModel user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
        };

        var roles = await this.userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var creds = new SigningCredentials(this.key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(5),
            SigningCredentials = creds,
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
