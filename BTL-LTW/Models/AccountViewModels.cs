using System.ComponentModel.DataAnnotations;

namespace BTL_LTW.Models
{
    /// <summary>
    /// Model người dùng hệ thống (Tài khoản)
    /// </summary>
    public class AccountUser
    {
        public int Id { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Mật khẩu")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Họ và tên")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Vai trò")]
        public string Role { get; set; } = "Sinh viên"; // Admin, Giảng viên, Sinh viên

        [Display(Name = "Mã sinh viên liên kết")]
        public string? StudentCode { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; } = "/images/students/avatar-1.svg";

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    /// <summary>
    /// ViewModel Đăng Nhập
    /// </summary>
    public class LoginViewModel
    {
        [Display(Name = "Tên đăng nhập hoặc Email")]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập hoặc email")]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Ghi nhớ đăng nhập")]
        public bool RememberMe { get; set; } = false;
    }

    /// <summary>
    /// ViewModel Đăng Ký Tài Khoản
    /// </summary>
    public class RegisterViewModel
    {
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Họ và tên từ 3 đến 100 ký tự")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [RegularExpression(@"^[a-zA-Z0-9_]{4,20}$", ErrorMessage = "Tên đăng nhập từ 4-20 ký tự, không dấu và không chứa ký tự đặc biệt")]
        public string Username { get; set; } = string.Empty;

        [Display(Name = "Địa chỉ Email")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Vui lòng xác nhận lại mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "Vai trò")]
        public string Role { get; set; } = "Sinh viên"; // Sinh viên, Giảng viên, Quản trị viên

        [Display(Name = "Đồng ý điều khoản")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Bạn cần đồng ý với điều khoản sử dụng")]
        public bool AgreeTerms { get; set; }
    }
}
