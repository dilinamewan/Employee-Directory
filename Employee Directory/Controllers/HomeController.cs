using System.Diagnostics;
using Employee_Directory.Models;
using Employee_Directory.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employee_Directory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Show dashboard statistics for all users (authenticated or not)
            try
            {
                ViewBag.TotalEmployees = await _context.Employees.CountAsync();
                ViewBag.RecentEmployees = await _context.Employees
                    .OrderByDescending(e => e.HireDate)
                    .Take(5)
                    .ToListAsync();
                
                var departmentCounts = await _context.Employees
                    .GroupBy(e => e.Department)
                    .Select(g => new { Department = g.Key, Count = g.Count() })
                    .ToListAsync();
                
                ViewBag.DepartmentCounts = departmentCounts;
                ViewBag.TotalDepartments = departmentCounts.Count;
                
                _logger.LogInformation("Dashboard data loaded - Total Employees: {Count}", (int)ViewBag.TotalEmployees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard data");
                ViewBag.TotalEmployees = 0;
                ViewBag.RecentEmployees = new List<Employee>();
                ViewBag.DepartmentCounts = new List<object>();
                ViewBag.TotalDepartments = 0;
            }

            return View();
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
    }
}
