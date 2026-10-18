using System.ComponentModel.DataAnnotations;

namespace BTL_LTW.Models
{
    /// <summary>
    /// Model Bảng điểm / Kết quả học phần
    /// </summary>
    public class Grade
    {
        [Key]
        [Display(Name = "Mã kết quả")]
        public int Id { get; set; }

        [Display(Name = "Sinh viên")]
        [Required(ErrorMessage = "Vui lòng chọn sinh viên")]
        public int StudentId { get; set; }

        [Display(Name = "Mã sinh viên")]
        public string StudentCode { get; set; } = string.Empty;

        [Display(Name = "Họ tên sinh viên")]
        public string StudentName { get; set; } = string.Empty;

        [Display(Name = "Môn học")]
        [Required(ErrorMessage = "Vui lòng chọn môn học")]
        public int CourseId { get; set; }

        [Display(Name = "Mã môn")]
        public string CourseCode { get; set; } = string.Empty;

        [Display(Name = "Tên môn học")]
        public string CourseName { get; set; } = string.Empty;

        [Display(Name = "Số tín chỉ")]
        public int Credits { get; set; } = 3;

        [Display(Name = "Điểm chuyên cần (10%)")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm từ 0 đến 10")]
        public double AttendanceScore { get; set; } = 10.0;

        [Display(Name = "Điểm giữa kỳ (30%)")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm từ 0 đến 10")]
        public double MidtermScore { get; set; } = 8.0;

        [Display(Name = "Điểm thi cuối kỳ (60%)")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm từ 0 đến 10")]
        public double FinalScore { get; set; } = 8.5;

        /// <summary>
        /// Điểm trung bình môn (Tự động tính: 10% CC + 30% GK + 60% CK)
        /// </summary>
        [Display(Name = "Điểm tổng kết")]
        public double AverageScore => Math.Round(AttendanceScore * 0.1 + MidtermScore * 0.3 + FinalScore * 0.6, 2);

        /// <summary>
        /// Quy đổi điểm chữ theo thang điểm 4/10 chuẩn Bộ GD&ĐT
        /// </summary>
        [Display(Name = "Điểm chữ")]
        public string LetterGrade
        {
            get
            {
                var avg = AverageScore;
                if (avg >= 9.0) return "A+";
                if (avg >= 8.5) return "A";
                if (avg >= 8.0) return "B+";
                if (avg >= 7.0) return "B";
                if (avg >= 6.5) return "C+";
                if (avg >= 5.5) return "C";
                if (avg >= 5.0) return "D+";
                if (avg >= 4.0) return "D";
                return "F";
            }
        }

        /// <summary>
        /// Đạt hay không đạt môn học (F là rớt)
        /// </summary>
        [Display(Name = "Kết quả")]
        public bool IsPassed => AverageScore >= 4.0;
    }
}
