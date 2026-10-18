using BTL_LTW.Data;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.ViewComponents
{
    /// <summary>
    /// ViewComponent hiển thị danh sách các Khoa trên thanh điều hướng Menu
    /// Theo đúng chuẩn kỹ thuật của bài thực hành Lab 03
    /// </summary>
    public class DepartmentNavViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var departments = DataStore.GetAllDepartments();
            return View(departments);
        }
    }
}
