using BTL_LTW.Data;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.ViewComponents
{
    /// <summary>
    /// ViewComponent vinh danh các sinh viên tiêu biểu có điểm GPA cao nhất
    /// </summary>
    public class TopStudentsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int count = 4)
        {
            var topStudents = DataStore.GetAllStudents()
                                       .OrderByDescending(s => s.GPA)
                                       .Take(count)
                                       .ToList();
            return View(topStudents);
        }
    }
}
