using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using Quizellent.Models;
using Quizellent.Utility;

namespace Quizellent.Controllers
{
    public class AuthController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor) : Controller
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (model == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Login attempt");
                return View();
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);

                if (_userManager == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
                    return View(model);
                }

                if (result.Succeeded)
                {
                    var userDetails = await Helper.GetCurrentUserIdAsync(_httpContextAccessor, _userManager);
                    return RedirectToAction("Index", "Page");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == model.Username || u.Email == model.Email);

                if (existingUser != null)
                {
                    return View();
                }

                var user = new IdentityUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Page");
                }
                return View();
            }
            return View();
        }
    }
}
