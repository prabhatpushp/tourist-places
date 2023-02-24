using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TravelPlaces.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace TravelPlaces.Controllers
{

    public class LoginController : Controller
    {

        private readonly Ace42023Context db;
        private readonly ISession session;
        public LoginController(Ace42023Context _db, IHttpContextAccessor httpContextAccessor)
        {
            db = _db;
            session = httpContextAccessor.HttpContext.Session;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usertbl u)
        {
            bool result = false;
            if (u.UserName == "Prabhat" && u.Password == "Welcome")
            {
                result = true;
            }
            if (result)
            {
                HttpContext.Session.SetString("username", u.UserName);
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}