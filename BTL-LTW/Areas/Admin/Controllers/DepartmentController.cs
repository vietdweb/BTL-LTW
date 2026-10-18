using BTL_LTW.Data;
using BTL_LTW.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            var departments = DataStore.GetAllDepartments();
            return View(departments);
        }

        public IActionResult Details(int id)
        {
            var dept = DataStore.GetDepartmentById(id);
            if (dept == null)
            {
                return NotFound();
            }

            ViewBag.Students = DataStore.GetAllStudents().Where(s => s.DepartmentId == id).ToList();
            ViewBag.Courses = DataStore.GetAllCourses().Where(c => c.DepartmentId == id).ToList();
            return View(dept);
        }

        public IActionResult Create()
        {
            return View(new Department { EstablishedDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (DataStore.GetAllDepartments().Any(d => d.DepartmentCode.Equals(department.DepartmentCode, StringComparison.OrdinalIgnoreCase)))
            {
                ModelState.AddModelError("DepartmentCode", "Mã khoa này đã tồn tại!");
            }

            if (ModelState.IsValid)
            {
                DataStore.AddDepartment(department);
                TempData["SuccessMessage"] = $"Thêm mới khoa '{department.Name}' thành công!";
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        public IActionResult Edit(int id)
        {
            var dept = DataStore.GetDepartmentById(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                DataStore.UpdateDepartment(department);
                TempData["SuccessMessage"] = $"Cập nhật thông tin khoa '{department.Name}' thành công!";
                return RedirectToAction(nameof(Index));
            }

            return View(department);
        }

        public IActionResult Delete(int id)
        {
            var dept = DataStore.GetDepartmentById(id);
            if (dept == null)
            {
                return NotFound();
            }
            return View(dept);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var dept = DataStore.GetDepartmentById(id);
            var name = dept?.Name ?? "Khoa";

            if (DataStore.DeleteDepartment(id))
            {
                TempData["SuccessMessage"] = $"Đã xóa thành công {name}!";
            }
            else
            {
                TempData["ErrorMessage"] = $"Không thể xóa {name} vì hiện tại vẫn còn sinh viên đang theo học tại khoa này!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
