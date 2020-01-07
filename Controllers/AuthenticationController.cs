using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SantoAndreOnBus.Filters;
using SantoAndreOnBus.Helpers;
using SantoAndreOnBus.Models.DTOs;

namespace SantoAndreOnBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtGenerator _jwt;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationController(JwtGenerator jwt,
                                        SignInManager<IdentityUser> signInManager,
                                        UserManager<IdentityUser> userManager)
        {
            _jwt = jwt;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [ValidateModelAndSendErrors]
        [Authorize]
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegistrationDTO request)
        {
            var user = new IdentityUser()
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) { return BadRequest(result.Errors); }
            await _signInManager.SignInAsync(user, false);

            return Ok(await _jwt.Generate(user.Email));
        }

        [ValidateModelAndSendErrors]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            var result = await _signInManager.PasswordSignInAsync(
                userName: request.Email,
                password: request.Password,
                isPersistent: false,
                lockoutOnFailure: true);

            if (!result.Succeeded) { return BadRequest("Nome de usuário ou senha inválido."); }
            var token = new TokenDTO { Token = await _jwt.Generate(request.Email) };

            return Ok(token);
        }
    }
}