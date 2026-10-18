using System.ComponentModel.DataAnnotations;

namespace BTL_LTW.Models
{
    /// <summary>
    /// Model Sinh viên với đầy đủ ràng buộc Data Annotations theo chuẩn Lab 05
    /// </summary>
    public class Student
    {
        [Key]
        [Display(Name = "Mã định danh")]
        public int Id { get; set; }

        [Display(Name = "Mã sinh viên")]
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [RegularExpression(@"^[A-Z0-9]{6,12}$", ErrorMessage = "Mã sinh viên gồm 6-12 ký tự chữ hoa hoặc số (VD: SV202401)")]
        public string StudentCode { get; set; } = string.Empty;

        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Họ và tên phải từ 3 đến 100 ký tự")]
        public string FullName { get; set; } = string.Empty;

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; } = new DateTime(2003, 1, 1);

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public int Gender { get; set; } = 1; // 1: Nam, 0: Nữ

        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng định dạng (VD: example@student.edu.vn)")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Số điện thoại phải là số hợp lệ ở VN (10 chữ số, bắt đầu bằng 03, 05, 07, 08, 09)")]
        public string Phone { get; set; } = string.Empty;

        [Display(Name = "Quê quán / Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ thường trú không được để trống")]
        [StringLength(250, ErrorMessage = "Địa chỉ không vượt quá 250 ký tự")]
        public string Address { get; set; } = string.Empty;

        [Display(Name = "Ảnh đại diện")]
        public string Avatar { get; set; } = "/images/students/default-avatar.svg";

        [Display(Name = "Khoa / Viện")]
        [Required(ErrorMessage = "Vui lòng chọn khoa phụ trách")]
        public int DepartmentId { get; set; }

        [Display(Name = "Tên khoa")]
        public string DepartmentName { get; set; } = string.Empty;

        [Display(Name = "Lớp sinh hoạt")]
        [Required(ErrorMessage = "Lớp sinh hoạt không được để trống")]
        [StringLength(50, ErrorMessage = "Tên lớp tối đa 50 ký tự")]
        public string ClassName { get; set; } = "CNTT-K15";

        [Display(Name = "Khóa / Năm nhập học")]
        [Range(2015, 2030, ErrorMessage = "Năm nhập học từ 2015 đến 2030")]
        public int EnrollmentYear { get; set; } = 2022;

        [Display(Name = "Trạng thái học tập")]
        public int Status { get; set; } = 1; // 1: Đang học, 2: Tốt nghiệp, 3: Bảo lưu, 4: Đình chỉ

        [Display(Name = "Điểm GPA (Hệ 10)")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm GPA phải từ 0.0 đến 10.0")]
        public double GPA { get; set; } = 8.0;

        /// <summary>
        /// Xếp loại học lực tự động tính theo GPA
        /// </summary>
        [Display(Name = "Xếp loại học lực")]
        public string AcademicRank
        {
            get
            {
                if (GPA >= 9.0) return "Xuất sắc";
                if (GPA >= 8.0) return "Giỏi";
                if (GPA >= 6.5) return "Khá";
                if (GPA >= 5.0) return "Trung bình";
                return "Yếu";
            }
        }

        /// <summary>
        /// Text hiển thị trạng thái sinh viên
        /// </summary>
        public string StatusText => Status switch
        {
            1 => "Đang học",
            2 => "Đã tốt nghiệp",
            3 => "Đang bảo lưu",
            4 => "Đình chỉ",
            _ => "Khác"
        };

        /// <summary>
        /// Màu sắc nhãn badge trạng thái chuẩn Sneat pastel
        /// </summary>
        public string StatusBadgeClass => Status switch
        {
            1 => "bg-label-success",
            2 => "bg-label-primary",
            3 => "bg-label-warning",
            4 => "bg-label-danger",
            _ => "bg-label-secondary"
        };

        /// <summary>
        /// Màu sắc nhãn badge học lực chuẩn Sneat pastel
        /// </summary>
        public string RankBadgeClass => AcademicRank switch
        {
            "Xuất sắc" => "bg-label-danger",
            "Giỏi" => "bg-label-success",
            "Khá" => "bg-label-info",
            "Trung bình" => "bg-label-warning",
            _ => "bg-label-secondary"
        };
    }
}
