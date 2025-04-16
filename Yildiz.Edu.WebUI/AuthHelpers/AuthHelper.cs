using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Yildiz.Edu.WebUI.DataAccess.Abstract;
using Yildiz.Edu.WebUI.Entities.Security;

namespace Yildiz.Edu.WebUI.AuthHelpers
{
    public class AuthHelper
    {
        IConfiguration configuration;
        IHttpContextAccessor httpContextAccessor;
        IUserDal userDal;

        public AuthHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUserDal userDal)
        {
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
            this.userDal = userDal;
        }


        private ICollection<Claim> GetUserClaims(User user)
        {

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.Email));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.FirstName + " " + user.LastName));



            var userClaims = userDal.GetUserOperationClaims(user.Id);

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
            var userExists = userDal.CheckUserToLogin(email, password);

            if (!userExists)
            {
                return false;
            }
            var user = userDal.GetUserByEmail(email, password);

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
