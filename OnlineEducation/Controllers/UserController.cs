using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineEducation.Controllers
{
    public class UserController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(HttpContext.Session.GetString("user") != null)
            {
                TempData["loginstatus"] = "true";
            }
            base.OnActionExecuting(context);
        }
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
