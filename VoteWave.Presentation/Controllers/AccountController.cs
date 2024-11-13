using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VoteWave.Application.Authorization.Commands;
using VoteWave.Application.Authorization.Exceptions;
using VoteWave.Presentation.Services.IServices;
using VoteWave.Presentation.ViewModels;
using VoteWave.Shared.Abstractions.Commands;

namespace VoteWave.Presentation.Controllers;

public class AccountController(ICommandDispatcher commandDispatcher,
    ITokenProvider tokenProvider) : Controller
{
    private readonly ICommandDispatcher _commandDispatcher = commandDispatcher;
    private readonly ITokenProvider _tokenProvider = tokenProvider;

    #region Register
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var command = new RegisterUser(model.Username, model.Email, model.Password);
        await _commandDispatcher.DispatchAsync(command);

        return RedirectToAction(nameof(Login));
    }
    #endregion

    #region Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var command = new AuthenticateUser(model.Username, model.Password);

        try
        {
            // Dispatch the command and get the JWT token as a result
            var token = await _commandDispatcher.DispatchAsync<AuthenticateUser, string>(command);

            // sign in user applied
            await SignInUser(token);

            // set token for user
            _tokenProvider.SetToken(token);

            return RedirectToAction("Index", "Home");
        }
        catch (InvalidCredentialsException)
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        _tokenProvider.ClearToken();
        return RedirectToAction("Index", "Home");
    }

    #region Private Methods
    // Sign In User
    private async Task SignInUser(string token)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwt = handler.ReadJwtToken(token);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        // adding claims
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
            jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
        identity.AddClaim(new Claim(ClaimTypes.Role,
            jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));


        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
    #endregion
    #endregion
}
