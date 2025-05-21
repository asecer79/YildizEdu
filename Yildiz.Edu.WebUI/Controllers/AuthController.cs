using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Yildiz.Edu.WebUI.Models.Dtos;

namespace Yildiz.Edu.WebUI.Controllers
{
    public class AuthController : Controller
    {
        HttpClient client;

        public AuthController()
        {
     
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:7136")

            };
        }

        [HttpGet]
        public async Task<IActionResult>  Login()
        {
            return await Task.FromResult<IActionResult>(View());
        }



        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {

            //      var response = await client.GetAsync($"api/Departments/Details/{id}");

            var response = await client.PostAsJsonAsync($"api/auth/login", new LoginRequestDto()
            {
                Email = email,
                Password= password

            } );

            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var result = JsonConvert.DeserializeObject<LoginResponseDto>(await response.Content.ReadAsStringAsync());

            //Response.Cookies.Append("access_token",result.Token, new CookieOptions
            //{
            //    HttpOnly = true,
            //    Secure = true,
            //});

            var handler = new JwtSecurityTokenHandler();

            var token = handler.ReadJwtToken(result.Token);

            var claims = token.Claims.ToList();

            claims.Add(new Claim("access_token",result.Token));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {

             await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public Task<IActionResult> Err()
        {
            

            return Task.FromResult<IActionResult>(View());
        }


    }
}
