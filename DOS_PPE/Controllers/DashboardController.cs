using DOS_PPE.Data;
using Microsoft.AspNetCore.Mvc;
using Rental.Controllers;

namespace DOS_PPE.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ApplicationDBContext _contextemp;
        private readonly SecondDbContext _context;

        public DashboardController(ApplicationDBContext contextemp, SecondDbContext context) : base(contextemp, context)
        {
            _context = context;
            _contextemp = contextemp;
        }

        public IActionResult Dashboard()
        {
            if (SetLoginNameInViewData()) return RedirectToAction("Error", "Home");

            return View();
        }

        public IActionResult Index_Approval()
        {
            if (SetLoginNameInViewData()) return RedirectToAction("Error", "Home");

            return View();
        }
    }
}
