using Microsoft.AspNetCore.Mvc;
using Yildiz.Edu.Security.AuthHelpers;
using Yildiz.Edu.WebAPI.Models;

namespace Yildiz.Edu.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AuthHelper authHelper) : ControllerBase
    {

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var token = authHelper.SignIn(loginRequestDto.Email, loginRequestDto.Password);

            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new { token });
        }
    }
}
