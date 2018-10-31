 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using projetfinalFJO.Appdata;
using projetfinalFJO.Models;
using projetfinalFJO.Models.Authentification;

namespace projetfinalFJO.Controllers
{

    [Authorize]
    public class LoginController : Controller
    {
        private readonly UserManager<LoginUser> _userManager;
        private readonly RoleManager<LoginRole> _roleManager;
        private readonly SignInManager<LoginUser> _signInManager;
        private readonly ILogger _logger;
        private readonly ActualisationContext context;

        public LoginController(
            UserManager<LoginUser> userManager,
            RoleManager<LoginRole> roleManager,
            SignInManager<LoginUser> signInManager,
            ILogger<LoginController> logger,
            ActualisationContext cont
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            this.context = cont;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl ?? "/Home/Index";
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    //TODO : Prendre les infos de l'utilisateur ici
                    this.context.Utilisateur.Select(user => user.AdresseCourriel == model.UserName);//
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.Roles = this._roleManager.Roles.Select<LoginRole, SelectListItem>(
                r =>
                new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name
                }
            ).ToList();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterViewModel model, string retrunUrl)
        {
            if (ModelState.IsValid)
            {
                var user = new LoginUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //
                    this.context.Utilisateur.Add(new Utilisateur { AdresseCourriel=model.Email, Nom=model.Nom, Prenom=model.Prenom, RegisterDate=new DateTime().Date});
                    this.context.SaveChanges();

                    _logger.LogInformation("User created a new account with password.");
                    var roleresult = await _userManager.AddToRoleAsync(user, model.Role);
                    if (roleresult.Succeeded)
                    {
                        _logger.LogInformation("User created a new account with password.");
                        return RedirectToAction(nameof(HomeController.Index), "Home");

                    }
                    else
                    {
                        ModelState.AddModelError("Erreur", roleresult.ToString());
                    }

                }
                else
                {
                    ModelState.AddModelError("Erreur", result.ToString());

                }

            }

            // If we got this far, something failed, redisplay form
            model.Roles = this._roleManager.Roles.Select<LoginRole, SelectListItem>(
                r =>
                new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name
                }
            ).ToList();
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewData["Url"] = returnUrl;
            return View();
        }

    }
}