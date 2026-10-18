using BTL_LTW.Data;
using BTL_LTW.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL_LTW.Controllers
{
    /// <summary>
    /// Controller quản lý xác thực: Đăng nhập, Đăng ký và Đăng xuất
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// GET: Trang Đăng Nhập
        /// </summary>
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            // Nếu đã đăng nhập rồi thì điều hướng thẳng
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                var role = HttpContext.Session.GetString("UserRole");
                if (role == "Admin")
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        /// <summary>
        /// POST: Xử lý Đăng Nhập
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = DataStore.ValidateUser(model.Username, model.Password);
                if (user != null)
                {
                    // Lưu thông tin người dùng vào Session
                    HttpContext.Session.SetString("Username", user.Username);
                    HttpContext.Session.SetString("FullName", user.FullName);
                    HttpContext.Session.SetString("UserRole", user.Role);
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Avatar", user.Avatar);
                    if (!string.IsNullOrEmpty(user.StudentCode))
                    {
                        HttpContext.Session.SetString("StudentCode", user.StudentCode);
                    }

                    TempData["SuccessMessage"] = $"Đăng nhập thành công! Chào mừng {user.FullName}.";

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    // Điều hướng theo vai trò người dùng
                    if (user.Role == "Admin")
                    {
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    }
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác!");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        /// <summary>
        /// GET: Trang Đăng Ký Tài Khoản
        /// </summary>
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        /// <summary>
        /// POST: Xử lý Đăng Ký
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newAccount = new AccountUser
                {
                    Username = model.Username.Trim(),
                    Password = model.Password,
                    FullName = model.FullName.Trim(),
                    Email = model.Email.Trim(),
                    Role = model.Role,
                    Avatar = "/images/students/default-avatar.svg"
                };

                bool success = DataStore.RegisterUser(newAccount);
                if (success)
                {
                    TempData["SuccessMessage"] = $"Đăng ký tài khoản '{model.Username}' thành công! Vui lòng đăng nhập.";
                    return RedirectToAction(nameof(Login));
                }

                ModelState.AddModelError("Username", "Tên đăng nhập hoặc địa chỉ email này đã tồn tại trong hệ thống!");
            }

            return View(model);
        }

        /// <summary>
        /// Xử lý Đăng Xuất
        /// </summary>
        [HttpPost, HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Bạn đã đăng xuất an toàn khỏi hệ thống!";
            return RedirectToAction(nameof(Login));
        }
    }
}
