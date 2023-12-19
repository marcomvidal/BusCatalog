using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Api.Infrastructure.Filters;

namespace SantoAndreOnBus.Api.Authentication;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthenticationController(
        ITokenService tokenService,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _tokenService = tokenService;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [ValidateModelAndSendErrors]
    [Authorize]
    [HttpPost("register")]
    public async Task<ActionResult> Register(UserRegistrationRequest request)
    {
        var user = new IdentityUser()
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password!);

        if (!result.Succeeded)
            return BadRequest(result.Errors);
        
        await _signInManager.SignInAsync(user, false);

        return Ok(await _tokenService.Generate(user.Email!));
    }

    [ValidateModelAndSendErrors]
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(
            userName: request.Email!,
            password: request.Password!,
            isPersistent: false,
            lockoutOnFailure: true);

        if (!result.Succeeded)
            return BadRequest("Nome de usuário ou senha inválido.");
        
        var token = new TokenResponse
        {
            Token = await _tokenService.Generate(request.Email!)
        };

        return Ok(token);
    }
}
