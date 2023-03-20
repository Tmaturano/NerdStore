using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NS.Identity.API.Models;

namespace NS.Identity.API.Controllers
{
    [Route("api/identity")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("new-account")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(UserRegister newUser)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new IdentityUser
            {
                UserName = newUser.Email,
                Email = newUser.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, newUser.Password);

            if (!result.Succeeded)
                return BadRequest();

            await _signInManager.SignInAsync(user, isPersistent: false);
            return Ok();
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Login(UserLogin login)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.IsLockedOut)
                return BadRequest("User blocked");

            if (!result.Succeeded)
                return BadRequest("User or password invalid");

            return Ok();
        }
    }
}
