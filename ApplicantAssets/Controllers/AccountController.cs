namespace ApplicantAssets.Api.Controllers;
using ApplicantAssets.Api.DTOs;
using ApplicantAssets.Api.Services;
using ApplicantAssets.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for managing user accounts.
/// </summary>
public class AccountController : BaseController
{
    private readonly UserManager<AppUserModel> userManager;
    private readonly SignInManager<AppUserModel> signInManager;
    private readonly ITokenService tokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="userManager">An api provider for managing app users.</param>
    /// <param name="signInManager">An api provider for managing sign in process.</param>
    /// <param name="tokenService">A service for generating authenticating token.</param>
    public AccountController(
        UserManager<AppUserModel> userManager,
        SignInManager<AppUserModel> signInManager,
        ITokenService tokenService)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.tokenService = tokenService;
    }

    /// <summary>
    /// Endpoint for logging in.
    /// </summary>
    /// <param name="login">A login dto.</param>
    /// <returns>A <see cref="Task"/> that represents an asynchronous operation.</returns>
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginDto login)
    {
        var user = await this.userManager.FindByNameAsync(login.Username);

        if (user is null)
        {
            return this.Unauthorized("Invalid username");
        }

        var result = await this.signInManager.CheckPasswordSignInAsync(user, login.Password, false);

        if (!result.Succeeded)
        {
            return this.Unauthorized("Invalid password");
        }

        return this.Ok(new UserDto
        {
            Username = user.UserName,
            Token = await this.tokenService.CreateTokenAsync(user),
        });
    }
}
