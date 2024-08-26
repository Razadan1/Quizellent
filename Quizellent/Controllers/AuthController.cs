using Microsoft.AspNetCore.Mvc;

namespace Quizellent.Controllers
{
    public class AuthController() : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (ModelState.IsValid)
            {
                return View();
            }
        }
    }
}
