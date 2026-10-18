using BTL_LTW.Data;
using BTL_LTW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StudentController : Controller
    {
        /// <summary>
        /// Hiển thị danh sách sinh viên kèm bộ lọc và tìm kiếm
        /// </summary>
        public IActionResult Index(string? keyword, int? departmentId, int? status)
        {
            ViewBag.Keyword = keyword;
            ViewBag.DepartmentId = departmentId;
            ViewBag.Status = status;

            // Nạp danh sách khoa cho dropdown lọc
            var departments = DataStore.GetAllDepartments();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", departmentId);

            var students = DataStore.SearchStudents(keyword, departmentId, status);
            return View(students);
        }

        /// <summary>
        /// Xem chi tiết hồ sơ sinh viên kèm bảng điểm
        /// </summary>
        public IActionResult Details(int id)
        {
            var student = DataStore.GetStudentById(id);
            if (student == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sinh viên yêu cầu!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Grades = DataStore.GetGradesByStudentId(id);
            ViewBag.Department = DataStore.GetDepartmentById(student.DepartmentId);
            return View(student);
        }

        /// <summary>
        /// GET: Form thêm mới sinh viên
        /// </summary>
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            var model = new Student
            {
                Birthday = new DateTime(2004, 1, 1),
                EnrollmentYear = DateTime.Now.Year,
                Status = 1,
                GPA = 8.0,
                Avatar = "/images/students/avatar-1.svg"
            };
            return View(model);
        }

        /// <summary>
        /// POST: Xử lý thêm mới sinh viên
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student, IFormFile? avatarFile)
        {
            // Kiểm tra trùng mã sinh viên
            if (DataStore.GetAllStudents().Any(s => s.StudentCode.Equals(student.StudentCode, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("StudentCode", "Mã sinh viên này đã tồn tại trong hệ thống!");
            }

            if (ModelState.IsValid)
            {
                // Xử lý upload ảnh nếu có
                if (avatarFile != null && avatarFile.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(avatarFile.FileName)}";
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "students");
                    
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    var filePath = Path.Combine(uploadDir, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        avatarFile.CopyTo(stream);
                    }
                    student.Avatar = $"/images/students/{fileName}";
                }

                DataStore.AddStudent(student);
                TempData["SuccessMessage"] = $"Thêm mới sinh viên {student.FullName} thành công!";
                return RedirectToAction(nameof(Index));
            }

            PopulateDepartmentsDropDownList(student.DepartmentId);
            return View(student);
        }

        /// <summary>
        /// GET: Form chỉnh sửa sinh viên
        /// </summary>
        public IActionResult Edit(int id)
        {
            var student = DataStore.GetStudentById(id);
            if (student == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sinh viên cần sửa!";
                return RedirectToAction(nameof(Index));
            }

            PopulateDepartmentsDropDownList(student.DepartmentId);
            return View(student);
        }

        /// <summary>
        /// POST: Xử lý lưu thông tin cập nhật
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student, IFormFile? avatarFile)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            // Kiểm tra trùng mã SV với người khác
            if (DataStore.GetAllStudents().Any(s => s.Id != id && s.StudentCode.Equals(student.StudentCode, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("StudentCode", "Mã sinh viên này đã bị trùng với sinh viên khác!");
            }

            if (ModelState.IsValid)
            {
                // Xử lý ảnh đại diện mới nếu có upload
                if (avatarFile != null && avatarFile.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(avatarFile.FileName)}";
                    var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "students");
                    
                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    var filePath = Path.Combine(uploadDir, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        avatarFile.CopyTo(stream);
                    }
                    student.Avatar = $"/images/students/{fileName}";
                }

                DataStore.UpdateStudent(student);
                TempData["SuccessMessage"] = $"Cập nhật sinh viên {student.FullName} thành công!";
                return RedirectToAction(nameof(Index));
            }

            PopulateDepartmentsDropDownList(student.DepartmentId);
            return View(student);
        }

        /// <summary>
        /// GET: Trang xác nhận xóa sinh viên
        /// </summary>
        public IActionResult Delete(int id)
        {
            var student = DataStore.GetStudentById(id);
            if (student == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy sinh viên cần xóa!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Department = DataStore.GetDepartmentById(student.DepartmentId);
            return View(student);
        }

        /// <summary>
        /// POST: Thực hiện xóa sinh viên
        /// </summary>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = DataStore.GetStudentById(id);
            var name = student?.FullName ?? "sinh viên";

            if (DataStore.DeleteStudent(id))
            {
                TempData["SuccessMessage"] = $"Đã xóa thành công hồ sơ của {name}!";
            }
            else
            {
                TempData["ErrorMessage"] = "Không thể xóa hồ sơ sinh viên!";
            }

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Remote Validation theo chuẩn Lab 05 để kiểm tra trùng mã SV ngay trên giao diện
        /// </summary>
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyStudentCode(string studentCode, int? id)
        {
            bool exists = DataStore.GetAllStudents().Any(s => s.StudentCode.Equals(studentCode, StringComparison.OrdinalIgnoreCase) && (!id.HasValue || s.Id != id.Value));
            if (exists)
            {
                return Json($"Mã sinh viên '{studentCode}' đã tồn tại!");
            }
            return Json(true);
        }

        private void PopulateDepartmentsDropDownList(object? selectedDepartment = null)
        {
            var departments = DataStore.GetAllDepartments();
            ViewBag.DepartmentId = new SelectList(departments, "Id", "Name", selectedDepartment);
        }
    }
}
