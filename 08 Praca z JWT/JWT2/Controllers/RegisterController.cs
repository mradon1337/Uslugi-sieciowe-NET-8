using BlogCMS.Auth;
using BlogCMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // nowe konta moze dodawac tylko zalogowany uzytkownik
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public ActionResult Register([FromBody] UserLogin newUser)
        {
            // nie pozwalamy na dwa konta z tym samym loginem
            bool alreadyExists = UserConstants.Users.Any(x =>
                x.Username.ToLower() == newUser.Username.ToLower());

            if (alreadyExists)
            {
                return Conflict("Uzytkownik o takim loginie juz istnieje");
            }

            // dla uproszczenia kazdy nowy uzytkownik dostaje role Admin
            UserConstants.Users.Add(new LoginModel
            {
                Username = newUser.Username,
                Password = newUser.Password,
                Role = "Admin"
            });

            return Ok("Uzytkownik zostal dodany");
        }
    }
}
