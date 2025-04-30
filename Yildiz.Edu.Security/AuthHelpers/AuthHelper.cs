using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Yildiz.Edu.Business.Abstract;
using Yildiz.Edu.Entities.Concrete.Security;

namespace Yildiz.Edu.Security.AuthHelpers
{
    public class AuthHelper
    {
        IConfiguration configuration;
        IHttpContextAccessor httpContextAccessor;
        IUserService userService;

        public AuthHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
            this.userService = userService;
        }


        private ICollection<Claim> GetUserClaims(User user)
        {

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.FirstName + " " + user.LastName));



            var userClaims = userService.GetUserOperationClaims(user.Id);

            if (userClaims != null)
            {
                foreach (var userClaim in userClaims)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userClaim.Name));

                }
            }


            return claims;
        }

        public async Task<bool> SignIn(string email, string password)
        {
            var userExists = userService.CheckUserToLogin(email, password);

            if (!userExists)
            {
                return false;
            }
            var user = userService.GetUserByEmail(email, password);

            var claims = GetUserClaims(user);

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return true;
        }

        public async Task<bool> SignOut()
        {
            await httpContextAccessor.HttpContext.SignOutAsync();

            return true;
        }
    }
}
