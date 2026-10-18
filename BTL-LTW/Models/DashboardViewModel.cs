namespace BTL_LTW.Models
{
    public class DashboardViewModel
    {
        public int TotalStudents { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalCourses { get; set; }
        public int TotalGrades { get; set; }

        public int ActiveStudents { get; set; }
        public int GraduatedStudents { get; set; }
        public int ReservedStudents { get; set; }

        public int ExcellentCount { get; set; }
        public int GoodCount { get; set; }
        public int FairCount { get; set; }
        public int AverageCount { get; set; }

        public List<Student> RecentStudents { get; set; } = new();
        public List<DepartmentStat> DepartmentStats { get; set; } = new();
    }

    public class DepartmentStat
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public double AverageGPA { get; set; }
    }
}
