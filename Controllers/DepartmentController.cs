using Employee_Management_System.Models;
using Employee_Management_System.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ConnectionString _db;

        public DepartmentController(ConnectionString db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<DepartmentViewModel> departments = await _db.Departments.Where(w => w.IsDeleted == false).Select(d => new DepartmentViewModel
            {
                ID = d.ID,
                Name = d.Name,
                Manager = d.DepartmentManager != null ? d.DepartmentManager.Name : "No Manager",
                Budget = d.Budget,
                Status = d.Status
            }).ToListAsync();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _db.Departments.AddAsync(department);
                    int save = await _db.SaveChangesAsync();

                    if (save == 0)
                        throw new Exception("Failed to Create Department!!!");

                    TempData["SuccessMessage"] = "Deprtment Created Successful!!!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                Department? department = await _db.Departments.FindAsync(id);
                ViewBag.Manager = await _db.Employees.Where(w => w.DepartmentID == id && w.IsDeleted == false).ToListAsync();

                if (department == null)
                    throw new Exception("Department not found!!!");

                return View(department);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(department).State = EntityState.Modified;

                    int save = await _db.SaveChangesAsync();

                    if (save > 0)
                        TempData["SuccessMessage"] = "Department Updated Successfully!!!";
                    else
                        TempData["ErrorMessage"] = "Error: Failed to Update Department!!!";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Department? department = await _db.Departments.FindAsync(id);

                if (department == null)
                    throw new Exception("Department not found!!!");

                department.IsDeleted = true;

                _db.Entry(department).State = EntityState.Modified;

                int save = await _db.SaveChangesAsync();

                if (save > 0)

                    TempData["SuccessMessage"] = "Department Deleted Successfully!!!";
                else
                    TempData["ErrorMessage"] = "Error: Failed to Delete Department!!!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
