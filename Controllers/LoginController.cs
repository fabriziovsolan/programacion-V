using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MvcMovie1.BaseDeDatosFicticia;
using System.Security.Claims;

namespace MvcMovie1.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = FakeUserDB.Users.FirstOrDefault(u => u.User == username && u.Password == password);

            if (user != null)
            {
                var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, username)
                             };
                
                HttpContext.Session.SetString("User", user.User);
                var Identity = new ClaimsIdentity(claims, "Cookies");
                var principal = new ClaimsPrincipal(Identity);

                HttpContext.SignInAsync("Cookies",  principal);

                // return RedirectToAction("Index","Home");  
                return RedirectToAction("Principal", "Principal");
            }
            else
            {
                ViewBag.Error = "Credenciales Inválidas";
            }
                return View();
        }
        public ActionResult Logout()
        {
            HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login");
        }

    }
}
