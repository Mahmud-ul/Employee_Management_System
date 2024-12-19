using Employee_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Employee_Management_System.Controllers
{
    public class PerformanceReviewController : Controller
    {
        private readonly ConnectionString _db;

        public PerformanceReviewController(ConnectionString db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<PerformanceReview> performances = await _db.PerformanceReviews.Where(w => w.IsDeleted == false).ToListAsync();

            ViewBag.Employee = await _db.Employees.ToListAsync();
            return View(performances);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Employee = await _db.Employees.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PerformanceReview performance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _db.PerformanceReviews.AddAsync(performance);
                    int save = await _db.SaveChangesAsync();

                    if (save == 0)
                        throw new Exception("Performance Review Entry Failed!!!");

                    TempData["SuccessMessage"] = "Performance Review Entry Successful!!!";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }
            ViewBag.Employee = await _db.Employees.ToListAsync();
            return View(performance);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                PerformanceReview? performance = await _db.PerformanceReviews.FindAsync(id);

                if (performance == null)
                    throw new Exception("Performance Review not found!!!");

                ViewBag.Employee = await _db.Employees.ToListAsync();
                return View(performance);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(PerformanceReview performance)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.Entry(performance).State = EntityState.Modified;

                    int save = await _db.SaveChangesAsync();

                    if (save > 0)
                        TempData["SuccessMessage"] = "Performance Review Updated Successfully!!!";
                    else
                        TempData["ErrorMessage"] = "Error: Failed to Update Performance Review!!!";
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
                PerformanceReview? performance = await _db.PerformanceReviews.FindAsync(id);

                if (performance == null)
                    throw new Exception("Performance Review not found!!!");

                performance.IsDeleted = true;

                _db.Entry(performance).State = EntityState.Modified;

                int save = await _db.SaveChangesAsync();

                if (save > 0)

                    TempData["SuccessMessage"] = "Performance Review Deleted Successfully!!!";
                else
                    TempData["ErrorMessage"] = "Error: Failed to Delete Performance Review!!!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error: " + ex.Message;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAvgPerformanceScorePerDepartment()
        {
            try
            {
                var connection = _db.Database.GetDbConnection();
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXEC sp_AvgPerformanceScorePerDepartment";
                    command.CommandType = System.Data.CommandType.Text;

                    var result = new List<dynamic>();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(new
                            {
                                DepartmentName = reader["DepartmentName"].ToString(),
                                AvgPerformanceScore = reader["AvgPerformanceScore"] != DBNull.Value
                                    ? Convert.ToDouble(reader["AvgPerformanceScore"])
                                    : (double?)null
                            });
                        }
                    }
                    TempData["result"] = JsonConvert.SerializeObject(result);
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
