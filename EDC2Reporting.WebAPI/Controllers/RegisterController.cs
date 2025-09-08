using DataServices.SqlServerRepository.Models;
using EDC2Reporting.WebAPI.Models.RegisterModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using EDC2Reporting.WebAPI.Models.LoginModels;

namespace EDC2Reporting.WebAPI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<Investigator> _userManager;
        private readonly SignInManager<Investigator> _signInManager;

        public RegisterController(UserManager<Investigator> userManager, SignInManager<Investigator> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Self-register (Anonymous allowed)
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new Investigator
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                QuickLookId = Guid.NewGuid().ToString("N").Substring(0, 6)
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // Invite user (only by ClinicalTrialLeader or SiteManager)
        [HttpPost]
        [Authorize(Roles = "ClinicalTrialLeader,SiteManager")]
        public async Task<IActionResult> Invite(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email required.");

            var user = new Investigator
            {
                UserName = email,
                Email = email,
                QuickLookId = Guid.NewGuid().ToString("N").Substring(0, 6)
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                // TODO: send email invitation with QuickLookId
                return Ok(new { Message = "Invitation sent", QuickLookId = user.QuickLookId });
            }

            return BadRequest(result.Errors);
        }
    }
}
