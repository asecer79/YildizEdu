using Microsoft.AspNetCore.Mvc;
using Yildiz.Edu.Security.AuthHelpers;

namespace Yildiz.Edu.WebUI.Controllers
{
    public class AuthController : Controller
    {
        AuthHelper authHelper;
        public AuthController(AuthHelper authHelper)
        {
            this.authHelper = authHelper;
        }
        [HttpGet]
        public async Task<IActionResult>  Login()
        {
            return await Task.FromResult<IActionResult>(View());
        }



        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var isAuthenticated = await authHelper.SignIn(email, password);

            if (isAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.message = "User cannot be found! Check username and password!";
            ViewBag.email = email;
            ViewBag.password = password;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await authHelper.SignOut();

            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public Task<IActionResult> Err()
        {
            

            return Task.FromResult<IActionResult>(View());
        }


    }
}
