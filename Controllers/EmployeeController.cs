using Employee_Management_System.Models;
using Employee_Management_System.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ConnectionString _db;

        public EmployeeController(ConnectionString db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _db.Employees.Where(w => w.IsDeleted == false)
                .Select(e => new EmployeeViewModel
                {
                    ID = e.ID,
                    Name = e.Name,
                    Email = e.Email,
                    Phone = e.Phone,
                    Position = e.Position,
                    Status = e.Status,
                    JoiningDate = e.JoiningDate,
                    Department = e.Department != null ? e.Department.Name : "No Department",
                    LatestScore = e.PerformanceReviews
                    .OrderByDescending(pr => pr.ReviewDate)
                    .Select(pr => (int?)pr.ReviewScore)
                    .FirstOrDefault()
                }).ToListAsync();

            ViewBag.Department = await _db.Departments.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> FilteredResults(
            string Name,
            int? Department,
            string Email,
            string Phone,
            string Position,
            int? MinScore,
            int? MaxScore,
            bool? Status)
        {
            var query = _db.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Name))
                query = query.Where(e => e.Name.Contains(Name));

            if (Department.HasValue)
                query = query.Where(e => e.DepartmentID == Department);

            if (!string.IsNullOrWhiteSpace(Email))
                query = query.Where(e => e.Email.Contains(Email));

            if (!string.IsNullOrWhiteSpace(Phone))
                query = query.Where(e => e.Phone.Contains(Phone));

            if (!string.IsNullOrWhiteSpace(Position))
                query = query.Where(e => e.Position.Contains(Position));

            if (MinScore.HasValue)
                query = query.Where(e => e.PerformanceReviews.Any(pr => pr.ReviewScore >= MinScore));

            if (MaxScore.HasValue)
                query = query.Where(e => e.PerformanceReviews.Any(pr => pr.ReviewScore <= MaxScore));

            if (Status.HasValue)
                query = query.Where(e => e.Status == Status);

            query = query.Where(e => e.IsDeleted == false);

            var employees = await query.Select(e => new
            {
                e.ID,
                e.Name,
                e.Email,
                e.Phone,
                e.Position,
                e.Status,
                e.JoiningDate,
                Department = e.Department.Name,
                LatestScore = e.PerformanceReviews
                    .OrderByDescending(pr => pr.ReviewDate)
                    .Select(pr => (int?)pr.ReviewScore)
                    .FirstOrDefault()
            }).ToListAsync();

            return Json(employees);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Department = await _db.Departments.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        await _db.Employees.AddAsync(employee);

                        var department = await _db.Departments.FindAsync(employee.DepartmentID);
                        if (department == null)
                            throw new Exception("Invalid Department ID!!!");

                        int save = await _db.SaveChangesAsync();

                        if (save == 0)
                            throw new Exception("Department Assigning Error!!!");

                        transaction.Commit();

                        TempData["SuccessMessage"] = "Employee Created and Assigned to Department Successfully!!!";

                        return RedirectToAction("Index");
                    }
                }   
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Error: " + ex.Message;
                }
                ViewBag.Department = await _db.Departments.ToListAsync();
                return View(employee);
            }         
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                Employee? employee = await _db.Employees.FindAsync(id);

                if (employee == null)
                    throw new Exception("Employee not found!!!");

                ViewBag.Department = await _db.Departments.ToListAsync();
                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(employee).State = EntityState.Modified;

                    int save = await _db.SaveChangesAsync();

                    if (save > 0)

                        TempData["SuccessMessage"] = "Employee Updated Successfully!!!";
                    else
                        TempData["ErrorMessage"] = "Error: Failed to Update the Employee!!!";
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
                Employee? employee = await _db.Employees.FindAsync(id);

                if (employee == null)
                    throw new Exception("Employee not found!!!");

                employee.IsDeleted = true;

                _db.Entry(employee).State = EntityState.Modified;

                int save = await _db.SaveChangesAsync();

                if(save > 0)

                    TempData["SuccessMessage"] = "Employee Deleted Successfully!!!";
                else
                    TempData["ErrorMessage"] = "Error: Failed to Delete the Employee!!!";      
            }
            catch (Exception ex) 
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
    }
}
