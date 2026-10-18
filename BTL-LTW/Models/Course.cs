using System.ComponentModel.DataAnnotations;

namespace BTL_LTW.Models
{
    /// <summary>
    /// Model đại diện cho Môn học / Học phần
    /// </summary>
    public class Course
    {
        [Key]
        [Display(Name = "Mã số")]
        public int Id { get; set; }

        [Display(Name = "Mã môn học")]
        [Required(ErrorMessage = "Vui lòng nhập mã môn học")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Mã môn từ 2 đến 20 ký tự")]
        public string CourseCode { get; set; } = string.Empty;

        [Display(Name = "Tên môn học")]
        [Required(ErrorMessage = "Vui lòng nhập tên môn học")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Tên môn học từ 3 đến 150 ký tự")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Số tín chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập số tín chỉ")]
        [Range(1, 10, ErrorMessage = "Số tín chỉ phải từ 1 đến 10")]
        public int Credits { get; set; } = 3;

        [Display(Name = "Khoa phụ trách")]
        [Required(ErrorMessage = "Vui lòng chọn khoa phụ trách")]
        public int DepartmentId { get; set; }

        [Display(Name = "Tên khoa")]
        public string DepartmentName { get; set; } = string.Empty;

        [Display(Name = "Học kỳ giảng dạy")]
        [StringLength(20, ErrorMessage = "Học kỳ tối đa 20 ký tự")]
        public string Semester { get; set; } = "Học kỳ 1";

        [Display(Name = "Mô tả môn học")]
        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
        public string Description { get; set; } = string.Empty;
    }
}
