# BÀI TẬP LỚN LẬP TRÌNH WEB (ASP.NET CORE MVC)
## ĐỀ TÀI: HỆ THỐNG QUẢN LÝ SINH VIÊN & ĐÀO TẠO (UNISTUDENT PORTAL)

---

### I. HƯỚNG DẪN CHẠY DỰ ÁN TRÊN VISUAL STUDIO (VS TÍM)
1. Mở file **`BTL-LTW.slnx`** hoặc thư mục dự án bằng **Visual Studio 2022 / 2025**.
2. Kiểm tra thanh công cụ phía trên (đang chọn profile `http` hoặc `https` hoặc `BTL-LTW`).
3. Nhấn phím **F5** (hoặc **Ctrl + F5**, hoặc bấm nút **Play màu xanh**).
4. Trình duyệt sẽ tự động bật lên website:
   - **Trang chủ Client:** `https://localhost:7225` hoặc `http://localhost:5021`
   - **Trang Quản trị Admin:** `https://localhost:7225/Admin` hoặc click vào nút **"Trang Quản Trị"** màu xanh trên thanh Menu.
5. **Dữ liệu chạy ngay lập tức (Zero Config):** Dự án đã được nạp sẵn 10+ sinh viên mẫu, 4 khoa viện và bảng điểm thực tế, không cần cài đặt SQL Server phức tạp. Mọi thao tác Thêm, Sửa, Xóa đều hoạt động thời gian thực.
6. **Tài khoản mẫu để kiểm tra đăng nhập (Mock Data):**
   - **Quản trị viên (Admin):** Tài khoản: `admin` | Mật khẩu: `123` (Truy cập toàn quyền Dashboard & CRUD)
   - **Giảng viên:** Tài khoản: `giangvien` | Mật khẩu: `123` (Xem thông tin, nhập điểm học phần)
   - **Sinh viên:** Tài khoản: `sinhvien` | Mật khẩu: `123` (Xem thông tin cá nhân, tra cứu bảng điểm)
   *(Giao diện Đăng nhập có sẵn 3 nút click tự động điền tài khoản mẫu rất tiện lợi khi báo cáo!)*

---

### II. KIẾN TRÚC DỰ ÁN (BÁM SÁT 100% TƯ DUY CỦA THẦY)

Dự án áp dụng toàn bộ các kỹ thuật đã học trong các bài Lab thực hành:
1. **Kiến trúc MVC:**
   - **Models (`Models/`):** 
     - `Student.cs`: Sinh viên (mã SV, họ tên, ngày sinh, khoa, lớp, GPA, xếp loại học lực, avatar...).
     - `Department.cs`: Khoa / Viện đào tạo (mã khoa, tên khoa, trưởng khoa, hotline, email...).
     - `Course.cs`: Môn học / Học phần (mã môn, tên môn, số tín chỉ, học kỳ...).
     - `Grade.cs`: Điểm học phần (chuyên cần 10%, giữa kỳ 30%, cuối kỳ 60%, điểm chữ A/B/C/D/F, xét Đạt/Rớt).
     - `DashboardViewModel.cs`: Thống kê KPI, biểu đồ phân bố học lực.
   - **Controllers (`Controllers/`):**
     - `HomeController.cs`: Trang chủ, tìm kiếm nhanh sinh viên, giới thiệu, liên hệ.
     - `StudentController.cs`: Xem danh sách sinh viên, chi tiết hồ sơ & bảng điểm, tra cứu điểm trực tuyến.
   - **Views (`Views/`):**
     - Razor View chuẩn, Tag Helpers (`asp-controller`, `asp-action`, `asp-for`, `asp-route-*`).
2. **Phân vùng Khu vực Quản trị (`Areas/Admin` - Lab 07):**
   - Bộ điều khiển có thuộc tính `[Area("Admin")]`.
   - `DashboardController.cs`: Bảng thống kê KPI số lượng sinh viên, khoa viện, tỷ lệ học lực xuất sắc/giỏi/khá/yếu.
   - `StudentController.cs`: CRUD đầy đủ (Thêm mới có upload ảnh đại diện, Sửa, Chi tiết bảng điểm, Xóa, Lọc theo Khoa/Trạng thái, Tìm kiếm).
   - `DepartmentController.cs`: CRUD quản lý Khoa Viện.
   - `GradeController.cs`: Nhập điểm và tính điểm trung bình GPA tự động.
   - `_AdminLayout.cshtml`: Giao diện Dashboard chuyên nghiệp, menu sidebar, alert thông báo.
3. **Thành phần tái sử dụng (`ViewComponents/` - Lab 03):**
   - `DepartmentNavViewComponent`: Tự động nạp menu danh mục Khoa lên thanh điều hướng Header.
   - `TopStudentsViewComponent`: Vinh danh Top sinh viên xuất sắc nhất có GPA cao lên trang chủ.
4. **Kiểm tra hợp lệ dữ liệu (`Data Annotations` - Lab 05):**
   - `[Required]`, `[StringLength]`, `[EmailAddress]`, `[RegularExpression]` (cho SĐT & Mã SV), `[Range]` (cho điểm số GPA 0-10), Remote Validation kiểm tra trùng lặp.
   - Toàn bộ thông báo lỗi bằng **tiếng Việt thân thiện, rõ ràng**.

---

### III. KỊCH BẢN BÁO CÁO 10 ĐIỂM CHO GIẢNG VIÊN (5 PHÚT)

* **Phút 1: Giới thiệu đề tài & Trang chủ Client**
  - Mở trang chủ: Giới thiệu hệ thống UniStudent Portal.
  - Cho thầy xem banner, thẻ thống kê tổng quan (Tổng SV, Khoa viện, Tỷ lệ việc làm).
  - Chỉ ra **ViewComponent Top sinh viên** và **ViewComponent Menu Khoa & Viện** trên Navbar.
  - Demo tính năng **"Tra cứu nhanh mã sinh viên"** ngay tại ô tìm kiếm giữa trang chủ (nhập thử `SV202201` bấm Tra cứu -> bay thẳng vào hồ sơ và bảng điểm).
* **Phút 2: Danh sách sinh viên & Tra cứu điểm trực tuyến**
  - Vào menu **"Sinh viên"**: Demo bộ lọc theo Khoa, ô tìm kiếm từ khóa.
  - Vào menu **"Tra cứu điểm"**: Nhập mã sinh viên `SV202202` để xem bảng điểm học phần (có điểm CC 10%, GK 30%, CK 60%, tổng kết và điểm chữ A, B, C...).
* **Phút 3: Cổng Quản Trị (Admin Area)**
  - Bấm nút **"Trang Quản Trị"** góc trên bên phải để vào `Areas/Admin`.
  - Giới thiệu **Bảng điều khiển (Dashboard)**: Biểu đồ thanh tiến trình phân bố học lực (Xuất sắc, Giỏi, Khá, Trung bình) và quy mô từng khoa.
* **Phút 4: Thao tác CRUD Quản lý Sinh viên**
  - Bấm **"Thêm sinh viên mới"**:
    - Nhập thiếu thông tin -> Bấm Lưu để thầy thấy **Validation tiếng Việt** chặn lỗi ngay lập tức.
    - Nhập thông tin đầy đủ, chọn Khoa, upload ảnh đại diện -> Bấm Lưu -> Hệ thống báo Alert xanh thành công và cập nhật ngay vào bảng.
  - Sửa thông tin hoặc Xóa một sinh viên.
* **Phút 5: Nhập điểm & Trả lời câu hỏi bảo vệ**
  - Vào menu **"Quản lý Điểm & Học tập"** -> Chọn sinh viên, chọn môn học, nhập điểm thi -> Hệ thống tự động tính điểm trung bình và cập nhật lại điểm GPA cho sinh viên đó.
  - Tự tin giải thích cấu trúc code MVC, phân vùng Area, cách viết DataStore và ViewComponent khi thầy hỏi.

---

### IV. CÂU HỎI THƯỜNG GẶP KHI BẢO VỆ & CÁCH TRẢ LỜI
1. **Hỏi: Em cấu hình Area Admin ở đâu?**
   - *Trả lời:* Trong file `Program.cs`, em đặt route `name: "areas", pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"` trước route `default`. Trong Controller thì khai báo thuộc tính `[Area("Admin")]`.
2. **Hỏi: ViewComponent khác gì so với Partial View?**
   - *Trả lời:* Partial View chỉ nhận dữ liệu từ Controller cha truyền xuống, còn ViewComponent có class C# riêng (`ViewComponent`), có phương thức `Invoke()` độc lập để tự truy vấn dữ liệu từ DataStore nên có thể nhúng vào bất kỳ trang nào mà không phụ thuộc Controller của trang đó.
3. **Hỏi: Dữ liệu được lưu trữ và tính GPA như thế nào?**
   - *Trả lời:* Dữ liệu được quản lý tập trung trong `DataStore.cs` với cơ chế Thread-safe. Khi nhập điểm mới cho môn học, hàm `RecalculateStudentGPA()` sẽ tự động tính theo công thức: Tổng (Điểm môn $\times$ Số tín chỉ) chia cho Tổng số tín chỉ.
