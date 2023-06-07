using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Linq;
using System;
using WebService.Model;

namespace WebService.Controllers
{
    public class UserController : Controller
    {
        ApplicationContext context;
        string encodedJwt;
        public UserController(ApplicationContext _context)
        {
            context = _context;
        }

        // тестовые данные вместо использования базы данных
        /*private List<User> people = new List<User>
        {
            new User {Login="qwerty123", Password="12345678", Role = "admin" },
            new User { Login="12345678", Password="12345678", Role = "user" }
        };*/
        [HttpGet("/token")]
        public IActionResult GetToken(string login, string password)
        {
            var identity = GetIdentity(login, password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };
            return new ObjectResult(response);
        }

        private ClaimsIdentity GetIdentity(string login, string password)
        {
            User person = context.UsersData.FirstOrDefault(x => x.Login == login && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
