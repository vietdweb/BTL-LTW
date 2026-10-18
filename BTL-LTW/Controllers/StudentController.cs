using BTL_LTW.Data;
using BTL_LTW.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.Controllers
{
    /// <summary>
    /// Controller phục vụ tra cứu thông tin sinh viên và bảng điểm phía người dùng (Client)
    /// </summary>
    public class StudentController : Controller
    {
        /// <summary>
        /// Danh sách sinh viên công khai (có tìm kiếm và lọc theo khoa)
        /// </summary>
        public IActionResult Index(string? keyword, int? departmentId)
        {
            ViewBag.Keyword = keyword;
            ViewBag.DepartmentId = departmentId;
            ViewBag.Departments = DataStore.GetAllDepartments();

            var students = DataStore.SearchStudents(keyword, departmentId, null);
            return View(students);
        }

        /// <summary>
        /// Xem chi tiết hồ sơ sinh viên kèm bảng điểm học tập
        /// </summary>
        public IActionResult Details(int id)
        {
            var student = DataStore.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            // Lấy danh sách điểm các môn của sinh viên này
            ViewBag.Grades = DataStore.GetGradesByStudentId(id);
            ViewBag.Department = DataStore.GetDepartmentById(student.DepartmentId);

            return View(student);
        }

        /// <summary>
        /// Trang tra cứu điểm trực tuyến
        /// </summary>
        [HttpGet]
        public IActionResult Lookup(string? studentCode)
        {
            if (string.IsNullOrWhiteSpace(studentCode))
            {
                return View();
            }

            var student = DataStore.GetStudentByCode(studentCode.Trim());
            if (student == null)
            {
                ViewBag.NotFoundMessage = $"Không tìm thấy dữ liệu điểm của sinh viên mang mã: {studentCode}";
                return View();
            }

            ViewBag.Grades = DataStore.GetGradesByStudentId(student.Id);
            return View(student);
        }
    }
}
