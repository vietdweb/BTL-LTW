using System.ComponentModel.DataAnnotations;

namespace BTL_LTW.Models
{
    /// <summary>
    /// Model đại diện cho Khoa / Viện đào tạo
    /// </summary>
    public class Department
    {
        [Key]
        [Display(Name = "Mã số")]
        public int Id { get; set; }

        [Display(Name = "Mã khoa")]
        [Required(ErrorMessage = "Vui lòng nhập mã khoa")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Mã khoa từ 2 đến 20 ký tự")]
        public string DepartmentCode { get; set; } = string.Empty;

        [Display(Name = "Tên khoa")]
        [Required(ErrorMessage = "Vui lòng nhập tên khoa")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên khoa từ 3 đến 100 ký tự")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Trưởng khoa")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên trưởng khoa")]
        [StringLength(100, ErrorMessage = "Tên trưởng khoa tối đa 100 ký tự")]
        public string Dean { get; set; } = string.Empty;

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng định dạng")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Mô tả giới thiệu")]
        [StringLength(500, ErrorMessage = "Mô tả không quá 500 ký tự")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Ngày thành lập")]
        [DataType(DataType.Date)]
        public DateTime EstablishedDate { get; set; } = DateTime.Now;

        [Display(Name = "Trạng thái")]
        public int Status { get; set; } = 1; // 1: Hoạt động, 0: Tạm ngưng

        [Display(Name = "Số sinh viên")]
        public int StudentCount { get; set; } = 0;
    }
}
