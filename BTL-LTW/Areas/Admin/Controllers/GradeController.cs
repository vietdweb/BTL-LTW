using BTL_LTW.Data;
using BTL_LTW.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GradeController : Controller
    {
        public IActionResult Index(int? studentId)
        {
            ViewBag.Students = new SelectList(DataStore.GetAllStudents(), "Id", "FullName", studentId);
            
            var grades = DataStore.Grades.AsEnumerable();
            if (studentId.HasValue && studentId.Value > 0)
            {
                grades = grades.Where(g => g.StudentId == studentId.Value);
                ViewBag.SelectedStudent = DataStore.GetStudentById(studentId.Value);
            }

            return View(grades.ToList());
        }

        public IActionResult Create(int? studentId)
        {
            var students = DataStore.GetAllStudents();
            var courses = DataStore.GetAllCourses();

            ViewBag.StudentId = new SelectList(students, "Id", "FullName", studentId);
            ViewBag.CourseId = new SelectList(courses, "Id", "Name");

            var model = new Grade();
            if (studentId.HasValue)
            {
                model.StudentId = studentId.Value;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Grade grade)
        {
            // Kiểm tra sinh viên đã có điểm môn này chưa
            if (DataStore.Grades.Any(g => g.StudentId == grade.StudentId && g.CourseId == grade.CourseId))
            {
                ModelState.AddModelError("", "Sinh viên này đã có điểm môn học được chọn! Không thể nhập trùng.");
            }

            if (ModelState.IsValid)
            {
                DataStore.AddGrade(grade);
                TempData["SuccessMessage"] = "Nhập điểm môn học cho sinh viên thành công!";
                return RedirectToAction(nameof(Index), new { studentId = grade.StudentId });
            }

            ViewBag.StudentId = new SelectList(DataStore.GetAllStudents(), "Id", "FullName", grade.StudentId);
            ViewBag.CourseId = new SelectList(DataStore.GetAllCourses(), "Id", "Name", grade.CourseId);
            return View(grade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, int? studentId)
        {
            DataStore.DeleteGrade(id);
            TempData["SuccessMessage"] = "Đã xóa kết quả học phần thành công!";
            return RedirectToAction(nameof(Index), new { studentId });
        }
    }
}
