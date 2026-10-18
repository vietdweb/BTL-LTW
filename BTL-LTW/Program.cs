namespace BTL_LTW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Đăng ký dịch vụ MVC Controllers và Views
            builder.Services.AddControllersWithViews();

            // Đăng ký dịch vụ Session để lưu trữ trạng thái đăng nhập
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(4);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Cấu hình pipeline xử lý HTTP request
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSession();
            app.UseAuthorization();

            // 1. Cấu hình route cho phân vùng Area (Admin) - Đúng theo chuẩn Lab 07 của thầy
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

            // 2. Cấu hình route mặc định cho trang Client
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
