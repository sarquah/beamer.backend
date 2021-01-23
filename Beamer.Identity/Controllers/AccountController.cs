using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Beamer.Identity.Models;
using Beamer.Identity.ViewModels;

namespace Beamer.Identity.Controllers
{
	[Authorize]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _applicationDbContext;
        private static bool _databaseChecked;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
        }

        //
        // POST: /Account/Register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            EnsureDatabaseCreated(_applicationDbContext);
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return Ok();
                }
                AddErrors(result);
            }

            // If we got this far, something failed.
            return BadRequest(ModelState);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            EnsureDatabaseCreated(_applicationDbContext);
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return Ok();
                }                
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }

            // If we got this far, something failed, redisplay form
            return BadRequest();
        }

        // The following code creates the database and schema if they don't exist.
        // This is a temporary workaround since deploying database through EF migrations is
        // not yet supported in this release.
        // Please see this http://go.microsoft.com/fwlink/?LinkID=615859 for more information on how to do deploy the database
        // when publishing your application.
        private static void EnsureDatabaseCreated(ApplicationDbContext context)
        {
            if (!_databaseChecked)
            {
                _databaseChecked = true;
                context.Database.EnsureCreated();
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
