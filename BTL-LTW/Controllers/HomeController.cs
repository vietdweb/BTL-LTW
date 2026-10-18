using BTL_LTW.Data;
using BTL_LTW.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BTL_LTW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Trang chủ cổng thông tin sinh viên
        /// </summary>
        public IActionResult Index()
        {
            var stats = DataStore.GetDashboardViewModel();
            ViewBag.Departments = DataStore.GetAllDepartments();
            ViewBag.TopStudents = DataStore.GetAllStudents().OrderByDescending(s => s.GPA).Take(4).ToList();
            return View(stats);
        }

        /// <summary>
        /// Tra cứu nhanh sinh viên từ thanh tìm kiếm trang chủ
        /// </summary>
        [HttpPost]
        public IActionResult QuickSearch(string studentCode)
        {
            if (string.IsNullOrWhiteSpace(studentCode))
            {
                TempData["SearchError"] = "Vui lòng nhập mã sinh viên để tra cứu!";
                return RedirectToAction(nameof(Index));
            }

            var student = DataStore.GetStudentByCode(studentCode.Trim());
            if (student == null)
            {
                TempData["SearchError"] = $"Không tìm thấy sinh viên nào với mã: {studentCode.Trim()}";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Details", "Student", new { id = student.Id });
        }

        /// <summary>
        /// Giới thiệu về hệ thống quản lý đào tạo
        /// </summary>
        public IActionResult About()
        {
            ViewBag.Departments = DataStore.GetAllDepartments();
            return View();
        }

        /// <summary>
        /// Thông tin liên hệ phòng đào tạo & công tác sinh viên
        /// </summary>
        public IActionResult Contact()
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
