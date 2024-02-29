using DOS_PPE.Data;
using DOS_PPE.Models;
using Microsoft.AspNetCore.Mvc;
using Rental.Controllers;
using System.Diagnostics;
using System.Text;

namespace DOS_PPE.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _contextemp;
        private readonly SecondDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, IConfiguration config, ApplicationDBContext contextemp, SecondDbContext context) : base(contextemp, context)
        {
            _logger = logger;

            _environment = environment;
            _config = config;
            _context = context;
            _contextemp = contextemp;
        }

        public IActionResult Index(string id)
        {
            string empid = "";
            HttpContext.Session.SetString("employee_id", "DEXT-166");
            if (HttpContext.Session.GetString("employee_id") == null)
            {
                byte[] base64Bytes = Convert.FromBase64String(id);
                empid = Encoding.UTF8.GetString(base64Bytes);

                HttpContext.Session.SetString("employee_id", empid);
            }

            if (SetLoginNameInViewData()) return RedirectToAction("Error", "Home");

            return RedirectToAction("Permission", "Permission");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult backtohome()
        {
            string employee_Login = HttpContext.Session.GetString("employee_id");

            byte[] bytesToEncode = Encoding.UTF8.GetBytes(employee_Login);
            string Id_encrypt = Convert.ToBase64String(bytesToEncode);
            
            string url = "https://omnia.dexon-technology.com/Home/backtohome?username=" + Id_encrypt;

            return Redirect(url);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            ViewData.Clear();
            TempData.Clear();

            return Redirect("https://omnia.dexon-technology.com");
        }
    }
}