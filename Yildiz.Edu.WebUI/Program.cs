using Microsoft.AspNetCore.Authentication.Cookies;
using Yildiz.Edu.WebUI.Utils;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddHttpContextAccessor();

        var cookieAuthOptions = builder.Configuration.GetSection("CookieAuthOptions").Get<CookieAuthOptions>();

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.LoginPath = cookieAuthOptions.LoginPath;
            options.LogoutPath = cookieAuthOptions.LogOutPath;
            options.AccessDeniedPath = cookieAuthOptions.AccessDeniedPath;
            options.ExpireTimeSpan = TimeSpan.FromDays(cookieAuthOptions.TimeOut);
            options.SlidingExpiration = true;
            options.Cookie.Name = cookieAuthOptions.Name;
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

        app.UseRouting();

        app.MapStaticAssets();

        app.UseCookiePolicy();

        app.UseAuthentication();

        app.UseAuthorization();

       

        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();


        app.Run();
    }
}
