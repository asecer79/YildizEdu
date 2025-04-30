using Microsoft.AspNetCore.Authentication.Cookies;
using Yildiz.Edu.Business.Abstract;
using Yildiz.Edu.Business.Concrete;
using Yildiz.Edu.DataAccess.Dal.Abstract;
using Yildiz.Edu.DataAccess.Dal.Concrete;
using Yildiz.Edu.Security.AuthHelpers;
using Yildiz.Edu.WebUI.AuthHelpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    //options.Filters.Add(new AuthorizeFilter());

});


builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379,defaultDatabase=0";
    //options.InstanceName= "YTUCache-";
});
//builder.Services.AddDbContext<UniEduDbContext>(options =>
//    options.UseSqlServer());

//di container
builder.Services.AddSingleton<IFacultyService, FacultyService>();
builder.Services.AddSingleton<IFacultyDal,FacultyDal>();

builder.Services.AddSingleton<IDepartmentService, DepartmentService>();
builder.Services.AddSingleton<IDepartmentDal, DepartmentDal>();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserDal,UserDal>();

builder.Services.AddScoped<AuthHelper>();



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
