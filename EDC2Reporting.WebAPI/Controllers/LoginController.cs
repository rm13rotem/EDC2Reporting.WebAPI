using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using EDC2Reporting.WebAPI.Models;
using System.Threading.Tasks;
using DataServices.SqlServerRepository.Models;
using EDC2Reporting.WebAPI.Models.LoginModels;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<Investigator> _signInManager;
        private readonly UserManager<Investigator> _userManager;

        public LoginController(SignInManager<Investigator> signInManager, UserManager<Investigator> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            Investigator user = null;

            if (!string.IsNullOrEmpty(model.GuidId))
            {
                user = await _userManager.FindByIdAsync(model.GuidId);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, lockoutOnFailure: false);
                    if (result.Succeeded)
                        return RedirectToLocal(model.ReturnUrl);
                }
            }
            else if (!string.IsNullOrEmpty(model.QuickLookId))
            {
                user = await _userManager.FindByNameAsync(model.QuickLookId);
                if (user != null)
                {
                    await _signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToLocal(model.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction("Index", "Home");
        }
    }
}
