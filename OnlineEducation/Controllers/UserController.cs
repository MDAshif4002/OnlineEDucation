using Microsoft.AspNetCore.Mvc;

namespace OnlineEducation.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Login", "Website");
            }
            return View();
        }
        public IActionResult Account()
        {
            return View();
        }
    }
}
