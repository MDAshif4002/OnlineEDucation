using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineEducation.Models;
using OnlineEducation.Services;

namespace OnlineEducation.Controllers
{
    public class WebsiteController : Controller
    {
        public AppDbContext _context;
        public IWebHostEnvironment _environment;
        public EmailSender _emailSender;
        public WebsiteController(AppDbContext context,IWebHostEnvironment environment,EmailSender emailSender)
        {
            _context = context;
            _environment = environment;
            _emailSender = emailSender;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                TempData["loginstatus"] = "true";
            }
            base.OnActionExecuting(context);
        }
        public IActionResult Index()
        {
            var data = _context.slider.ToList();
            var alldata = new HomePage
            {
                slider = data
            };
            return View(alldata);
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Courses()
        {
            var data = _context.managecourse.ToList();
            return View(data);
        }
        public IActionResult PurchaseNow()
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return RedirectToAction("Login");
            }

            string userid = HttpContext.Session.GetString("userid");
            var data = _context.register.Find(int.Parse(userid));
            return View(data);
        }
        [HttpPost]
        public IActionResult PurchaseNow(IFormCollection form)
        {
            purchase p = new purchase();
            p.name = form["name"];
            p.email = form["email"];
            p.mobile = form["mobile"];
            p.city = form["city"];
            p.pincode = form["pincode"];
            p.address = form["address"];
            p.paymentmode = form["paymentmode"];

            string userid = HttpContext.Session.GetString("userid");
            p.userid = userid;

            _context.purchase.Add(p);
            _context.SaveChanges();
            return RedirectToAction("PurchaseNow");

        }
        public IActionResult Paynow()
        {
            return View();
        }
        public IActionResult OrderPlaced()
        {
            return View();
        }
        public IActionResult PaymentFailed()
        {
            return View();
        }
        public IActionResult Reader()
        {
            var data = _context.reader.ToList();
            return View(data);
        }
        public IActionResult Contacts()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ManageContact(managecontact contact)
        {
                _context.managecontact.Add(contact);
                _context.SaveChanges();
                return RedirectToAction("Contacts");
        }

        public IActionResult Login()
        {
            return View();
        }
       
        [HttpPost]
        public IActionResult Login(string email,string password)
        {
            var data = _context.register.FirstOrDefault(x => x.email == email && x.password == password && x.deleteduser != true);
            if(data != null)
            {
                HttpContext.Session.SetString("user", email);
                HttpContext.Session.SetString("userid", data.id.ToString());

                TempData["loginstatus"] = "true";

                return RedirectToAction("Index", "User");
            }
            else
            {
                TempData["msg"] = "Email or Password is Incorrect";
                return RedirectToAction("Login");
            }
        }
        public IActionResult Account()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(managechangepassword pass)
        {
            _context.managechangepassword.Add(pass);
            _context.SaveChanges();
            return RedirectToAction("ChangePassword");
        }
        public IActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveRegister(register reg, string email)
        {
            
            var data = _context.register.FirstOrDefault(x => x.email == reg.email);
            if (data != null)
            {
                //otp start
                Random rnd = new Random();
                int num = rnd.Next(10000, 99999);

                HttpContext.Session.SetString("otp", num.ToString());
                HttpContext.Session.SetString("email", email);

                string sendto = email;
                string subject = "OTP for Registration";
                string mail = "Dear User, OTP for Registration is-"+num;
                await _emailSender.SendEmailAsync(sendto, subject, mail);
                //otp end

                _context.register.Add(reg);
                _context.SaveChanges();

                return RedirectToAction("RegOtp", "Website");
            }
            else
            {
                TempData["msg"] = "OTP is Incorrect";
                return RedirectToAction("Register" , "Website");
            }
        }

        public IActionResult RegOtp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RegOtp(string otp)
        {
            string originalotp = HttpContext.Session.GetString("otp");
            if(otp == originalotp)
            {
                string email = HttpContext.Session.GetString("email");
                var data = _context.register.FirstOrDefault(x =>x.email == email);
                data.otp = otp;
                _context.register.Update(data);
                _context.SaveChanges();
                HttpContext.Session.Clear();

                return RedirectToAction("Login","Website");
            }
            else
            {
                TempData["msg"] = "Entered Incorrect OTP";
                return RedirectToAction("RegOtp","Website");
            }
        }
        public IActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(string email,  string password)
        {
            var data = _context.adminlogin.FirstOrDefault( x => x.email == email && x.password == password );
            if (data != null)
            {
                HttpContext.Session.SetString("admin", email);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["msg"] = "Email or Password is incorrect";
                return RedirectToAction("AdminLogin");
            }
        }
    }
}