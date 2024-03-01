using DOS_PPE.Data;
using DOS_PPE.Models;
using Microsoft.AspNetCore.Mvc;
using Rental.Controllers;
using System.Xml.Linq;

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

            //List modal add member
            var QSMS = _contextemp.Employee.Where(q => q.department == "QSMS" && q.status_active == "on").ToList();

            //Table show data permission
            var permissions = _context.Permission.ToList();
            var employees = _contextemp.Employee.Where(q => q.status_active == "on").ToList();
            var Permis = (from perm in permissions
                          join emp in employees on perm.employee_id equals emp.employee_id
                          where(emp.employee_id == perm.employee_id)
                          select new ApermisModel
                          {
                              employee_id = perm.employee_id,
                              name_en = emp.name_en,
                              profile_pic = emp.profile_pic,
                              emp_position = emp.emp_position,
                              department = emp.department,
                              role_emp = perm.role_emp,
                              Modify_date = perm.Modify_date,
                              status_active = emp.status_active,
                              ID_permis = perm.ID_permis,
                          }).ToList();

            var model = new PermisViewModel
            {
                Employees = QSMS,
                Apermis = Permis
            };

            return View(model);
        }

        //--- Add data
        public IActionResult _AddPer(string empid, string level)
        {
            try
            {
                var permis = _context.Permission.Where(t => t.employee_id == empid).FirstOrDefault();
                if (permis != null)
                {
                    return Json(false);
                }
                else
                {
                    var permis_add = new PermissionModel
                    {
                        employee_id = empid,
                        role_emp = level,
                        Modify_date= DateTime.Now,
                    };

                    _context.Permission.Add(permis_add);
                    _context.SaveChanges();

                    return Json(true);

                }

            }
            catch (Exception ex)
            {
                writeEventLog(ex.Message);
                return Json(false);

            }
        }

        //--- SHOW DATA EDIT
        public IActionResult _ShowEdit(int editid)
        {
            try
            {
                var admin = _context.Permission.FirstOrDefault(a => a.ID_permis == editid);

                if (admin != null)
                {
                    var data = new
                    {
                        Perid = admin.ID_permis,
                        levels = admin.role_emp
                    };

                    return Json(data);
                }
                return Json(null);
            }
            catch (Exception ex)
            {
                writeEventLog(ex.Message);
                return Json(false);
            }
        }


        //--- EDIT Admin
        public IActionResult _EditPer(int ePerid, string elevels)
        {
            try
            {
                if (SetLoginNameInViewData()) return RedirectToAction("Error", "Home");

                if (ePerid == 0 || elevels == null)
                {
                    return Json(false);
                }

                var admin = _context.Permission.FirstOrDefault(a => a.ID_permis == ePerid);
                if (admin.employee_id == ViewData["employee_id"] as string)
                {
                    return Json(false);
                }

                DateTime DateNow = DateTime.Now;
                if (admin != null)
                {
                    admin.role_emp = elevels;
                    admin.Modify_date = DateNow;

                    _context.SaveChanges();
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                writeEventLog(ex.Message);
                return Json(false);
            }
        }

        //--- DELETE Admin 
        public IActionResult _Delete(int id)
        {
            try
            {
                if (SetLoginNameInViewData()) return RedirectToAction("Error", "Home");

                var admin = _context.Permission.SingleOrDefault(p => p.ID_permis == id);
                if (admin != null)
                {
                    if (admin.employee_id == ViewData["employee_id"] as string)
                    {
                        return Json(false);
                    }
                    else
                    {
                        _context.Permission.Remove(admin);
                        
                        _context.SaveChanges();
                        return Json(true);
                    }
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception ex)
            {
                writeEventLog(ex.Message);
                return Json(false);
            }

        }

    }
}
