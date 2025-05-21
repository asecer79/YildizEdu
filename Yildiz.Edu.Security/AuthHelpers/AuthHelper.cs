using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Yildiz.Edu.Business.Abstract;
using Yildiz.Edu.Entities.Concrete.Security;

namespace Yildiz.Edu.Security.AuthHelpers
{
    public class AuthHelper
    {
        IConfiguration configuration;
        IUserService userService;

        public AuthHelper(IConfiguration configuration,  IUserService userService)
        {
            this.configuration = configuration;
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

        public string SignIn(string email, string password)
        {
            var userExists = userService.CheckUserToLogin(email, password);

            if (!userExists)
            {
                return null;
            }
            var user = userService.GetUserByEmail(email, password);

            var claims = GetUserClaims(user);

            var jwtSettings = configuration.GetSection("JwtSettings");

            //Convert.ToDouble(jwtSettings["ExpireTime"]

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireTime"])),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
         
            return tokenHandler.WriteToken(token);

        }

    }
}
