using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Yildiz.Edu.Business.DependencyResolvers;
using Yildiz.Edu.Security.AuthHelpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMemoryCache();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379,defaultDatabase=0";
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacYildizEduServicesModule());
});

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
