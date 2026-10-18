using BTL_LTW.Data;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        /// <summary>
        /// Trang chủ bảng điều khiển quản trị (Dashboard)
        /// </summary>
        public IActionResult Index()
        {
            var viewModel = DataStore.GetDashboardViewModel();
            return View(viewModel);
        }
    }
}
