using System.Net;

namespace MyOwnSummary_WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            builder.Services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Login/Authetication", "");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller=Home}/{action=Index}/{id?}");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Authetication}/{action=Login}/{id?}");
            });

            app.Run();

            //var builder = WebApplication.CreateBuilder(args);

            //// Configure Kestrel to listen on a specific port and IP address
            //builder.WebHost.ConfigureKestrel(serverOptions =>
            //{
            //    serverOptions.Listen(IPAddress.Any, 5000); // You can choose any port you like
            //});

            //// Add services to the container.
            //builder.Services.AddControllersWithViews();
            //builder.Services.AddSession();
            //builder.Services.AddMvc().AddRazorPagesOptions(options =>
            //{
            //    options.Conventions.AddPageRoute("/Login/Authetication", "");
            //});

            //var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseSession();

            ////app.MapControllerRoute(
            ////    name: "default",
            ////    pattern: "{controller=Home}/{action=Index}/{id?}");
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //name: "default",
            //pattern: "{controller=Authetication}/{action=Login}/{id?}");
            //});

            //app.Run();
        }
    }
}