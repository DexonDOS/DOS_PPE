using DOS_PPE.Data;
using Microsoft.AspNetCore.Mvc;
using Rental.Controllers;

namespace DOS_PPE.Controllers
{
    public class PermissionController : BaseController
    {
        private readonly ApplicationDBContext _contextemp;
        private readonly SecondDbContext _context;

        public PermissionController(ApplicationDBContext contextemp, SecondDbContext context) : base(contextemp, context)
        {
            _context = context;
            _contextemp = contextemp;
        }

        public IActionResult Permission()
        {
            if (SetLoginNameInViewData()) return RedirectToAction("Error", "Home");

            return View();
        }
    }
}
