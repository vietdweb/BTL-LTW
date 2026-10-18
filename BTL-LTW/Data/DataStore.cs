using BTL_LTW.Models;

namespace BTL_LTW.Data
{
    /// <summary>
    /// Lớp lưu trữ và quản lý dữ liệu tập trung (Data Store)
    /// Đảm bảo ứng dụng chạy ngay lập tức khi nhấn F5 trên Visual Studio mà không cần cấu hình SQL Server phức tạp.
    /// Code viết mạch lạc, rõ ràng, có đầy đủ các hàm CRUD để sinh viên dễ dàng đọc hiểu và báo cáo.
    /// </summary>
    public static class DataStore
    {
        // Danh sách lưu trữ trong bộ nhớ (Thread-safe lock)
        private static readonly object _lock = new();

        public static List<Department> Departments { get; private set; } = new();
        public static List<Course> Courses { get; private set; } = new();
        public static List<Student> Students { get; private set; } = new();
        public static List<Grade> Grades { get; private set; } = new();
        public static List<AccountUser> Accounts { get; private set; } = new();

        // Khởi tạo dữ liệu mẫu ban đầu
        static DataStore()
        {
            SeedInitialData();
        }

        private static void SeedInitialData()
        {
            // 1. Dữ liệu Khoa / Viện
            Departments = new List<Department>
            {
                new Department
                {
                    Id = 1,
                    DepartmentCode = "CNTT",
                    Name = "Khoa Công nghệ Thông tin",
                    Dean = "PGS.TS. Trần Đình Khang",
                    Phone = "02438692463",
                    Email = "fit@edu.vn",
                    Description = "Đào tạo kỹ sư CNTT hàng đầu về Công nghệ phần mềm, Trí tuệ nhân tạo, An toàn thông tin.",
                    EstablishedDate = new DateTime(1995, 3, 10),
                    Status = 1
                },
                new Department
                {
                    Id = 2,
                    DepartmentCode = "QTKD",
                    Name = "Khoa Quản trị Kinh doanh",
                    Dean = "TS. Nguyễn Thị Thu Thủy",
                    Phone = "02438691523",
                    Email = "qtkd@edu.vn",
                    Description = "Đào tạo cử nhân kinh tế chất lượng cao, marketing, logistics và quản trị doanh nghiệp.",
                    EstablishedDate = new DateTime(2000, 8, 15),
                    Status = 1
                },
                new Department
                {
                    Id = 3,
                    DepartmentCode = "DTVT",
                    Name = "Khoa Điện tử - Viễn thông",
                    Dean = "PGS.TS. Lê Minh Hoàng",
                    Phone = "02438687744",
                    Email = "dtvt@edu.vn",
                    Description = "Đào tạo chuyên sâu về mạch tích hợp, hệ thống nhúng, IoT và viễn thông thế hệ mới.",
                    EstablishedDate = new DateTime(1998, 5, 20),
                    Status = 1
                },
                new Department
                {
                    Id = 4,
                    DepartmentCode = "NNA",
                    Name = "Khoa Ngôn ngữ Anh",
                    Dean = "TS. Phạm Quỳnh Trang",
                    Phone = "02438693355",
                    Email = "nna@edu.vn",
                    Description = "Đào tạo cử nhân tiếng Anh thương mại, biên phiên dịch và sư phạm tiếng Anh.",
                    EstablishedDate = new DateTime(2005, 9, 1),
                    Status = 1
                }
            };

            // 2. Dữ liệu Môn học
            Courses = new List<Course>
            {
                new Course { Id = 1, CourseCode = "INT301", Name = "Lập trình Web ASP.NET Core MVC", Credits = 3, DepartmentId = 1, DepartmentName = "Khoa Công nghệ Thông tin", Semester = "Học kỳ 1", Description = "Phát triển ứng dụng web hiện đại với C#, .NET và kiến trúc MVC." },
                new Course { Id = 2, CourseCode = "INT201", Name = "Cấu trúc dữ liệu và giải thuật", Credits = 4, DepartmentId = 1, DepartmentName = "Khoa Công nghệ Thông tin", Semester = "Học kỳ 1", Description = "Các cấu trúc dữ liệu nền tảng và kỹ thuật tối ưu hóa thuật toán." },
                new Course { Id = 3, CourseCode = "INT205", Name = "Hệ quản trị cơ sở dữ liệu SQL Server", Credits = 3, DepartmentId = 1, DepartmentName = "Khoa Công nghệ Thông tin", Semester = "Học kỳ 2", Description = "Thiết kế CSDL, chuẩn hóa quan hệ và truy vấn SQL nâng cao." },
                new Course { Id = 4, CourseCode = "BUS101", Name = "Kinh tế vi mô", Credits = 3, DepartmentId = 2, DepartmentName = "Khoa Quản trị Kinh doanh", Semester = "Học kỳ 1", Description = "Nguyên lý hành vi của người tiêu dùng và doanh nghiệp trong nền kinh tế." },
                new Course { Id = 5, CourseCode = "BUS202", Name = "Marketing căn bản", Credits = 3, DepartmentId = 2, DepartmentName = "Khoa Quản trị Kinh doanh", Semester = "Học kỳ 2", Description = "Chiến lược tiếp thị sản phẩm, phân tích thị trường và khách hàng." },
                new Course { Id = 6, CourseCode = "TEL101", Name = "Kỹ thuật mạch điện tử", Credits = 4, DepartmentId = 3, DepartmentName = "Khoa Điện tử - Viễn thông", Semester = "Học kỳ 1", Description = "Phân tích và thiết kế các mạch khuếch đại, mạch số và cảm biến." },
                new Course { Id = 7, CourseCode = "ENG201", Name = "Tiếng Anh chuyên ngành CNTT", Credits = 3, DepartmentId = 4, DepartmentName = "Khoa Ngôn ngữ Anh", Semester = "Học kỳ 2", Description = "Thuật ngữ và kỹ năng giao tiếp tiếng Anh trong môi trường công nghệ." }
            };

            // 3. Dữ liệu Sinh viên mẫu
            Students = new List<Student>
            {
                new Student
                {
                    Id = 1,
                    StudentCode = "SV202201",
                    FullName = "Nguyễn Văn Hưng",
                    Birthday = new DateTime(2003, 4, 15),
                    Gender = 1,
                    Email = "hung.nv202201@student.edu.vn",
                    Phone = "0987123456",
                    Address = "Ba Đình, Hà Nội",
                    Avatar = "/images/students/avatar-1.svg",
                    DepartmentId = 1,
                    DepartmentName = "Khoa Công nghệ Thông tin",
                    ClassName = "CNTT1-K15",
                    EnrollmentYear = 2022,
                    Status = 1,
                    GPA = 9.2
                },
                new Student
                {
                    Id = 2,
                    StudentCode = "SV202202",
                    FullName = "Trần Thị Mai Anh",
                    Birthday = new DateTime(2003, 8, 22),
                    Gender = 0,
                    Email = "maianh.tt@student.edu.vn",
                    Phone = "0912345678",
                    Address = "Cầu Giấy, Hà Nội",
                    Avatar = "/images/students/avatar-2.svg",
                    DepartmentId = 1,
                    DepartmentName = "Khoa Công nghệ Thông tin",
                    ClassName = "CNTT1-K15",
                    EnrollmentYear = 2022,
                    Status = 1,
                    GPA = 8.8
                },
                new Student
                {
                    Id = 3,
                    StudentCode = "SV202203",
                    FullName = "Lê Hoàng Long",
                    Birthday = new DateTime(2003, 11, 5),
                    Gender = 1,
                    Email = "long.lh202203@student.edu.vn",
                    Phone = "0934567890",
                    Address = "Hải Châu, Đà Nẵng",
                    Avatar = "/images/students/avatar-3.svg",
                    DepartmentId = 2,
                    DepartmentName = "Khoa Quản trị Kinh doanh",
                    ClassName = "QTKD2-K15",
                    EnrollmentYear = 2022,
                    Status = 1,
                    GPA = 8.1
                },
                new Student
                {
                    Id = 4,
                    StudentCode = "SV202204",
                    FullName = "Phạm Thùy Linh",
                    Birthday = new DateTime(2003, 2, 18),
                    Gender = 0,
                    Email = "linh.pt202204@student.edu.vn",
                    Phone = "0976543210",
                    Address = "Quận 1, TP. Hồ Chí Minh",
                    Avatar = "/images/students/avatar-4.svg",
                    DepartmentId = 4,
                    DepartmentName = "Khoa Ngôn ngữ Anh",
                    ClassName = "NNA1-K15",
                    EnrollmentYear = 2022,
                    Status = 1,
                    GPA = 9.4
                },
                new Student
                {
                    Id = 5,
                    StudentCode = "SV202205",
                    FullName = "Vũ Đình Trọng",
                    Birthday = new DateTime(2002, 10, 12),
                    Gender = 1,
                    Email = "trong.vd202205@student.edu.vn",
                    Phone = "0945678123",
                    Address = "Thành phố Vinh, Nghệ An",
                    Avatar = "/images/students/avatar-5.svg",
                    DepartmentId = 3,
                    DepartmentName = "Khoa Điện tử - Viễn thông",
                    ClassName = "DTVT1-K14",
                    EnrollmentYear = 2021,
                    Status = 1,
                    GPA = 7.6
                },
                new Student
                {
                    Id = 6,
                    StudentCode = "SV202206",
                    FullName = "Hoàng Ngọc Ánh",
                    Birthday = new DateTime(2003, 6, 30),
                    Gender = 0,
                    Email = "anh.hn202206@student.edu.vn",
                    Phone = "0967891234",
                    Address = "Hồng Bàng, Hải Phòng",
                    Avatar = "/images/students/avatar-6.svg",
                    DepartmentId = 2,
                    DepartmentName = "Khoa Quản trị Kinh doanh",
                    ClassName = "QTKD1-K15",
                    EnrollmentYear = 2022,
                    Status = 1,
                    GPA = 8.5
                },
                new Student
                {
                    Id = 7,
                    StudentCode = "SV202207",
                    FullName = "Đặng Quang Huy",
                    Birthday = new DateTime(2003, 12, 14),
                    Gender = 1,
                    Email = "huy.dq202207@student.edu.vn",
                    Phone = "0982345671",
                    Address = "Thanh Xuân, Hà Nội",
                    Avatar = "/images/students/avatar-7.svg",
                    DepartmentId = 1,
                    DepartmentName = "Khoa Công nghệ Thông tin",
                    ClassName = "CNTT2-K15",
                    EnrollmentYear = 2022,
                    Status = 1,
                    GPA = 8.2
                },
                new Student
                {
                    Id = 8,
                    StudentCode = "SV202108",
                    FullName = "Ngô Minh Tuấn",
                    Birthday = new DateTime(2002, 3, 25),
                    Gender = 1,
                    Email = "tuan.nm202108@student.edu.vn",
                    Phone = "0918765432",
                    Address = "Bắc Ninh",
                    Avatar = "/images/students/avatar-8.svg",
                    DepartmentId = 1,
                    DepartmentName = "Khoa Công nghệ Thông tin",
                    ClassName = "CNTT1-K14",
                    EnrollmentYear = 2021,
                    Status = 2, // Đã tốt nghiệp
                    GPA = 8.9
                },
                new Student
                {
                    Id = 9,
                    StudentCode = "SV202309",
                    FullName = "Bùi Phương Thảo",
                    Birthday = new DateTime(2004, 5, 8),
                    Gender = 0,
                    Email = "thao.bp202309@student.edu.vn",
                    Phone = "0938123456",
                    Address = "Hạ Long, Quảng Ninh",
                    Avatar = "/images/students/avatar-9.svg",
                    DepartmentId = 4,
                    DepartmentName = "Khoa Ngôn ngữ Anh",
                    ClassName = "NNA1-K16",
                    EnrollmentYear = 2023,
                    Status = 1,
                    GPA = 7.9
                },
                new Student
                {
                    Id = 10,
                    StudentCode = "SV202210",
                    FullName = "Đỗ Quốc Bảo",
                    Birthday = new DateTime(2003, 7, 19),
                    Gender = 1,
                    Email = "bao.dq202210@student.edu.vn",
                    Phone = "0972345678",
                    Address = "Nam Định",
                    Avatar = "/images/students/avatar-10.svg",
                    DepartmentId = 3,
                    DepartmentName = "Khoa Điện tử - Viễn thông",
                    ClassName = "DTVT1-K15",
                    EnrollmentYear = 2022,
                    Status = 3, // Bảo lưu
                    GPA = 6.4
                }
            };

            // 4. Bảng điểm mẫu
            Grades = new List<Grade>
            {
                new Grade { Id = 1, StudentId = 1, StudentCode = "SV202201", StudentName = "Nguyễn Văn Hưng", CourseId = 1, CourseCode = "INT301", CourseName = "Lập trình Web ASP.NET Core MVC", Credits = 3, AttendanceScore = 10, MidtermScore = 9.5, FinalScore = 9.0 },
                new Grade { Id = 2, StudentId = 1, StudentCode = "SV202201", StudentName = "Nguyễn Văn Hưng", CourseId = 2, CourseCode = "INT201", CourseName = "Cấu trúc dữ liệu và giải thuật", Credits = 4, AttendanceScore = 9.5, MidtermScore = 9.0, FinalScore = 9.2 },
                new Grade { Id = 3, StudentId = 1, StudentCode = "SV202201", StudentName = "Nguyễn Văn Hưng", CourseId = 3, CourseCode = "INT205", CourseName = "Hệ quản trị cơ sở dữ liệu SQL Server", Credits = 3, AttendanceScore = 10, MidtermScore = 9.0, FinalScore = 9.5 },

                new Grade { Id = 4, StudentId = 2, StudentCode = "SV202202", StudentName = "Trần Thị Mai Anh", CourseId = 1, CourseCode = "INT301", CourseName = "Lập trình Web ASP.NET Core MVC", Credits = 3, AttendanceScore = 10, MidtermScore = 8.5, FinalScore = 8.8 },
                new Grade { Id = 5, StudentId = 2, StudentCode = "SV202202", StudentName = "Trần Thị Mai Anh", CourseId = 2, CourseCode = "INT201", CourseName = "Cấu trúc dữ liệu và giải thuật", Credits = 4, AttendanceScore = 9.0, MidtermScore = 8.0, FinalScore = 9.0 },

                new Grade { Id = 6, StudentId = 3, StudentCode = "SV202203", StudentName = "Lê Hoàng Long", CourseId = 4, CourseCode = "BUS101", CourseName = "Kinh tế vi mô", Credits = 3, AttendanceScore = 9.0, MidtermScore = 8.0, FinalScore = 8.0 },
                new Grade { Id = 7, StudentId = 3, StudentCode = "SV202203", StudentName = "Lê Hoàng Long", CourseId = 5, CourseCode = "BUS202", CourseName = "Marketing căn bản", Credits = 3, AttendanceScore = 8.5, MidtermScore = 8.0, FinalScore = 8.2 },

                new Grade { Id = 8, StudentId = 4, StudentCode = "SV202204", StudentName = "Phạm Thùy Linh", CourseId = 7, CourseCode = "ENG201", CourseName = "Tiếng Anh chuyên ngành CNTT", Credits = 3, AttendanceScore = 10, MidtermScore = 9.5, FinalScore = 9.5 },
                new Grade { Id = 9, StudentId = 4, StudentCode = "SV202204", StudentName = "Phạm Thùy Linh", CourseId = 4, CourseCode = "BUS101", CourseName = "Kinh tế vi mô", Credits = 3, AttendanceScore = 10, MidtermScore = 9.0, FinalScore = 9.2 }
            };

            // 5. Dữ liệu Tài khoản Mock Đăng nhập
            Accounts = new List<AccountUser>
            {
                new AccountUser
                {
                    Id = 1,
                    Username = "admin",
                    Password = "123",
                    FullName = "Quản Trị Viên",
                    Email = "admin@student.edu.vn",
                    Role = "Admin",
                    Avatar = "/images/students/avatar-1.svg"
                },
                new AccountUser
                {
                    Id = 2,
                    Username = "giangvien",
                    Password = "123",
                    FullName = "PGS.TS. Trần Đình Khang",
                    Email = "khang.td@edu.vn",
                    Role = "Giảng viên",
                    Avatar = "/images/students/avatar-5.svg"
                },
                new AccountUser
                {
                    Id = 3,
                    Username = "sinhvien",
                    Password = "123",
                    FullName = "Nguyễn Văn Hưng",
                    Email = "hung.nv202201@student.edu.vn",
                    Role = "Sinh viên",
                    StudentCode = "SV202201",
                    Avatar = "/images/students/avatar-1.svg"
                }
            };

            // Cập nhật số lượng sinh viên cho từng khoa
            UpdateDepartmentStudentCounts();
        }

        #region Account Authentication Methods
        public static AccountUser? ValidateUser(string username, string password)
        {
            lock (_lock)
            {
                return Accounts.FirstOrDefault(a => 
                    (a.Username.Equals(username, StringComparison.OrdinalIgnoreCase) || 
                     a.Email.Equals(username, StringComparison.OrdinalIgnoreCase)) &&
                    a.Password == password);
            }
        }

        public static bool RegisterUser(AccountUser newAccount)
        {
            lock (_lock)
            {
                if (Accounts.Any(a => a.Username.Equals(newAccount.Username, StringComparison.OrdinalIgnoreCase) ||
                                      a.Email.Equals(newAccount.Email, StringComparison.OrdinalIgnoreCase)))
                {
                    return false;
                }

                int newId = Accounts.Count > 0 ? Accounts.Max(a => a.Id) + 1 : 1;
                newAccount.Id = newId;
                Accounts.Add(newAccount);
                return true;
            }
        }

        public static AccountUser? GetAccountByUsername(string username)
        {
            lock (_lock)
            {
                return Accounts.FirstOrDefault(a => a.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            }
        }
        #endregion

        public static void UpdateDepartmentStudentCounts()
        {
            foreach (var dept in Departments)
            {
                dept.StudentCount = Students.Count(s => s.DepartmentId == dept.Id);
            }
        }

        #region Student CRUD & Search Methods
        public static List<Student> GetAllStudents()
        {
            lock (_lock)
            {
                return Students.OrderByDescending(s => s.Id).ToList();
            }
        }

        public static Student? GetStudentById(int id)
        {
            lock (_lock)
            {
                return Students.FirstOrDefault(s => s.Id == id);
            }
        }

        public static Student? GetStudentByCode(string code)
        {
            lock (_lock)
            {
                return Students.FirstOrDefault(s => s.StudentCode.Equals(code, StringComparison.OrdinalIgnoreCase));
            }
        }

        public static List<Student> SearchStudents(string? keyword, int? departmentId, int? status)
        {
            lock (_lock)
            {
                var query = Students.AsQueryable();

                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    var kw = keyword.Trim().ToLower();
                    query = query.Where(s => s.FullName.ToLower().Contains(kw) || 
                                             s.StudentCode.ToLower().Contains(kw) || 
                                             s.ClassName.ToLower().Contains(kw) ||
                                             s.Email.ToLower().Contains(kw));
                }

                if (departmentId.HasValue && departmentId.Value > 0)
                {
                    query = query.Where(s => s.DepartmentId == departmentId.Value);
                }

                if (status.HasValue && status.Value > 0)
                {
                    query = query.Where(s => s.Status == status.Value);
                }

                return query.OrderByDescending(s => s.Id).ToList();
            }
        }

        public static void AddStudent(Student student)
        {
            lock (_lock)
            {
                int newId = Students.Count > 0 ? Students.Max(s => s.Id) + 1 : 1;
                student.Id = newId;

                var dept = Departments.FirstOrDefault(d => d.Id == student.DepartmentId);
                if (dept != null)
                {
                    student.DepartmentName = dept.Name;
                }

                Students.Add(student);
                UpdateDepartmentStudentCounts();
            }
        }

        public static bool UpdateStudent(Student student)
        {
            lock (_lock)
            {
                var existing = Students.FirstOrDefault(s => s.Id == student.Id);
                if (existing == null) return false;

                existing.FullName = student.FullName;
                existing.Birthday = student.Birthday;
                existing.Gender = student.Gender;
                existing.Email = student.Email;
                existing.Phone = student.Phone;
                existing.Address = student.Address;
                existing.DepartmentId = student.DepartmentId;
                existing.ClassName = student.ClassName;
                existing.EnrollmentYear = student.EnrollmentYear;
                existing.Status = student.Status;
                existing.GPA = student.GPA;

                if (!string.IsNullOrEmpty(student.Avatar))
                {
                    existing.Avatar = student.Avatar;
                }

                var dept = Departments.FirstOrDefault(d => d.Id == student.DepartmentId);
                if (dept != null)
                {
                    existing.DepartmentName = dept.Name;
                }

                UpdateDepartmentStudentCounts();
                return true;
            }
        }

        public static bool DeleteStudent(int id)
        {
            lock (_lock)
            {
                var student = Students.FirstOrDefault(s => s.Id == id);
                if (student != null)
                {
                    Students.Remove(student);
                    // Xóa điểm tương ứng
                    Grades.RemoveAll(g => g.StudentId == id);
                    UpdateDepartmentStudentCounts();
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region Department CRUD Methods
        public static List<Department> GetAllDepartments()
        {
            lock (_lock)
            {
                UpdateDepartmentStudentCounts();
                return Departments.ToList();
            }
        }

        public static Department? GetDepartmentById(int id)
        {
            lock (_lock)
            {
                UpdateDepartmentStudentCounts();
                return Departments.FirstOrDefault(d => d.Id == id);
            }
        }

        public static void AddDepartment(Department department)
        {
            lock (_lock)
            {
                int newId = Departments.Count > 0 ? Departments.Max(d => d.Id) + 1 : 1;
                department.Id = newId;
                Departments.Add(department);
            }
        }

        public static bool UpdateDepartment(Department department)
        {
            lock (_lock)
            {
                var existing = Departments.FirstOrDefault(d => d.Id == department.Id);
                if (existing == null) return false;

                existing.DepartmentCode = department.DepartmentCode;
                existing.Name = department.Name;
                existing.Dean = department.Dean;
                existing.Phone = department.Phone;
                existing.Email = department.Email;
                existing.Description = department.Description;
                existing.Status = department.Status;
                existing.EstablishedDate = department.EstablishedDate;

                // Cập nhật tên khoa trong danh sách sinh viên
                foreach (var s in Students.Where(s => s.DepartmentId == department.Id))
                {
                    s.DepartmentName = department.Name;
                }

                return true;
            }
        }

        public static bool DeleteDepartment(int id)
        {
            lock (_lock)
            {
                var dept = Departments.FirstOrDefault(d => d.Id == id);
                if (dept != null)
                {
                    // Nếu khoa còn sinh viên, không cho xóa để đảm bảo toàn vẹn dữ liệu
                    if (Students.Any(s => s.DepartmentId == id))
                    {
                        return false;
                    }
                    Departments.Remove(dept);
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region Course & Grade Methods
        public static List<Course> GetAllCourses()
        {
            lock (_lock)
            {
                return Courses.ToList();
            }
        }

        public static Course? GetCourseById(int id)
        {
            lock (_lock)
            {
                return Courses.FirstOrDefault(c => c.Id == id);
            }
        }

        public static List<Grade> GetGradesByStudentId(int studentId)
        {
            lock (_lock)
            {
                return Grades.Where(g => g.StudentId == studentId).ToList();
            }
        }

        public static void AddGrade(Grade grade)
        {
            lock (_lock)
            {
                int newId = Grades.Count > 0 ? Grades.Max(g => g.Id) + 1 : 1;
                grade.Id = newId;

                var student = Students.FirstOrDefault(s => s.Id == grade.StudentId);
                if (student != null)
                {
                    grade.StudentCode = student.StudentCode;
                    grade.StudentName = student.FullName;
                }

                var course = Courses.FirstOrDefault(c => c.Id == grade.CourseId);
                if (course != null)
                {
                    grade.CourseCode = course.CourseCode;
                    grade.CourseName = course.Name;
                    grade.Credits = course.Credits;
                }

                Grades.Add(grade);

                // Tự động tính lại GPA cho sinh viên
                RecalculateStudentGPA(grade.StudentId);
            }
        }

        public static void DeleteGrade(int id)
        {
            lock (_lock)
            {
                var grade = Grades.FirstOrDefault(g => g.Id == id);
                if (grade != null)
                {
                    int studentId = grade.StudentId;
                    Grades.Remove(grade);
                    RecalculateStudentGPA(studentId);
                }
            }
        }

        private static void RecalculateStudentGPA(int studentId)
        {
            var student = Students.FirstOrDefault(s => s.Id == studentId);
            if (student == null) return;

            var studentGrades = Grades.Where(g => g.StudentId == studentId).ToList();
            if (studentGrades.Count > 0)
            {
                double totalCredits = studentGrades.Sum(g => g.Credits);
                double totalWeightedScore = studentGrades.Sum(g => g.AverageScore * g.Credits);
                student.GPA = Math.Round(totalWeightedScore / totalCredits, 2);
            }
        }
        #endregion

        #region Dashboard Statistics
        public static DashboardViewModel GetDashboardViewModel()
        {
            lock (_lock)
            {
                var vm = new DashboardViewModel
                {
                    TotalStudents = Students.Count,
                    TotalDepartments = Departments.Count,
                    TotalCourses = Courses.Count,
                    TotalGrades = Grades.Count,

                    ActiveStudents = Students.Count(s => s.Status == 1),
                    GraduatedStudents = Students.Count(s => s.Status == 2),
                    ReservedStudents = Students.Count(s => s.Status == 3),

                    ExcellentCount = Students.Count(s => s.GPA >= 9.0),
                    GoodCount = Students.Count(s => s.GPA >= 8.0 && s.GPA < 9.0),
                    FairCount = Students.Count(s => s.GPA >= 6.5 && s.GPA < 8.0),
                    AverageCount = Students.Count(s => s.GPA < 6.5),

                    RecentStudents = Students.OrderByDescending(s => s.Id).Take(5).ToList(),

                    DepartmentStats = Departments.Select(d => new DepartmentStat
                    {
                        DepartmentName = d.Name,
                        StudentCount = Students.Count(s => s.DepartmentId == d.Id),
                        AverageGPA = Students.Where(s => s.DepartmentId == d.Id).Any() ? 
                                     Math.Round(Students.Where(s => s.DepartmentId == d.Id).Average(s => s.GPA), 2) : 0
                    }).ToList()
                };

                return vm;
            }
        }
        #endregion
    }
}
