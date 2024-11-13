using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineEducation.Models;

namespace OnlineEducation.Controllers
{
    public class AdminController : Controller
    {
        public AppDbContext _context;
        public IWebHostEnvironment _environment;
        public AdminController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Index", "Website");
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Website");
        }
        public IActionResult Slider()
        {
            if (HttpContext.Session.GetString("admin") == null)
            {
                return RedirectToAction("Index", "Website");
            }
            var data = _context.slider.ToList();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> AddSlider(IFormFile image)
        {
            string folderpath = Path.Combine(_environment.WebRootPath, "slider");
            string filename = image.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var stream = new FileStream(filepath, FileMode.Create);
            await image.CopyToAsync(stream);

            slider s = new slider();
            s.image = filename;

            _context.slider.Add(s);
            _context.SaveChanges();
            return RedirectToAction("Slider");
        }
        public IActionResult DeleteSlider(int id)
        {
            var data = _context.slider.Find(id);
            string filename = data.image;
            if (filename != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "slider");
                string filepath = Path.Combine(folderpath, filename);
                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }
            _context.slider.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Slider");
        }
        public IActionResult ManageUser()
        {
            var data = _context.register.Where(x =>x.deleteduser == null || x.deleteduser == false).ToList();
            return View(data);
        }
        public IActionResult UserSoftDelete(int id)
        {
            var data = _context.register.Find(id);
            data.deleteduser = true;
             _context.register.Update(data);
            _context.SaveChanges();
            TempData["Delete"] = "Deleted Successfully";
            TempData["alert"] = "Your data Deleted Successfully";
            return RedirectToAction("ManageUser");
        }
        public IActionResult DeletedUser()
        {
            var data = _context.register.Where(x => x.deleteduser == true).ToList();
            return View(data);
        }
        public IActionResult DeleteUser(int id)
        {
            var data = _context.register.Find(id);
            data.deleteduser = true;
            _context.register.Remove(data);
            _context.SaveChanges();
            TempData["Delete"] = "Deleted Successfully";
            TempData["alert"] = "Your data Deleted Successfully";
            return RedirectToAction("DeletedUser");
        }
        public IActionResult RestoreUser(int id)
        {
            var data = _context.register.Find(id);
            data.deleteduser = false;
            _context.register.Update(data);
            _context.SaveChanges();
            //TempData["Delete"] = "Deleted Successfully";
            //TempData["alert"] = "Your data Deleted Successfully";
            return RedirectToAction("DeletedUser");
        }

        public IActionResult ManageCategory()
        {
            var data = _context.managecategory.ToList();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> ManageCategory(managecategory c, IFormFile image)
        {
            string folderpath = Path.Combine(_environment.WebRootPath, "category");
            string filename = image.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var filestream = new FileStream(filepath, FileMode.Create);
            await image.CopyToAsync(filestream);
            c.image = filename;

            _context.managecategory.Add(c);
            _context.SaveChanges();
            return RedirectToAction("ManageCategory");
        }
        public IActionResult ManageCategoryDelete(int id)
        {
            var data = _context.managecategory.Find(id);
            string filename = data.image;
            if(filename != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "category");
                string filepath = Path.Combine(folderpath, filename);
                if(System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }
            _context.managecategory.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("ManageCategory");
        }
        public IActionResult ManageReader()
        {
            var data = _context.reader.ToList();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> ManageReader(reader r , IFormFile readerpic)
        {
            string folderpath = Path.Combine(_environment.WebRootPath, "ReaderPicture");
            string filename = readerpic.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var filestream = new FileStream(filepath , FileMode.Create);
            await readerpic.CopyToAsync(filestream);
            r.readerpic = filename;

            _context.reader.Add(r);
            _context.SaveChanges();
            return RedirectToAction("ManageReader");
        }
        public IActionResult DeleteReader(int id)
        {
            var data = _context.reader.Find(id);
            string filename = data.readerpic;
            if(filename != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "ReaderPicture");
                string filepath = Path.Combine(folderpath, filename);
                if(System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }
            _context.reader.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("ManageReader");
        }
        public IActionResult ManageCourse()
        {
            var data = _context.managecourse.ToList();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> ManageCourse(managecourse c, IFormFile coursepic)
        {
            string folderpath = Path.Combine(_environment.WebRootPath, "CoursePicture");
            string filename = coursepic.FileName;
            string filepath = Path.Combine(folderpath, filename);
            var filestream = new FileStream(filepath, FileMode.Create);
            await coursepic.CopyToAsync(filestream);
            c.coursepic = filename;

            _context.managecourse.Add(c);
            _context.SaveChanges();
            return RedirectToAction("ManageCourse");
        }
        public IActionResult DeleteCourse(int id)
        {
            var data = _context.managecourse.Find(id);
            string filename = data.coursepic;
            if(data != null)
            {
                string folderpath = Path.Combine(_environment.WebRootPath, "CoursePicture");
                string filepath = Path.Combine(folderpath, filename);
                if(System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);
                }
            }
            _context.managecourse.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("ManageCourse", "Admin");
        }
        public IActionResult ManageContact()
        {
            var data = _context.managecontact.ToList();
            return View(data); 
        }
        public IActionResult DeleteContact(int id)
        {
            var data = _context.managecontact.Find(id);
            _context.managecontact.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("ManageContact" , "Admin");
        }
        public IActionResult PurchaseNow()
        {
            var data = _context.purchase.ToList();
            return View(data); 
        }
        public IActionResult PurchaseDelete(int id)
        {
            var data = _context.purchase.Find(id);
            _context.purchase.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("PurchaseNow", "Admin");
        }
    }
}