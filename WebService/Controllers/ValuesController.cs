using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Ваш логин: {User.Identity.Name}");
        }

        [Authorize]
        [Route("getrole")]
        public IActionResult GetRole()
        {
            string role = User.IsInRole("admin") ? "Администратор" : "Пользователь"; 
            return Ok($"Ваша роль: {role}");
        }
        
    }
}
