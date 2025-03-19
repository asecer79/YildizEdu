using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Yildiz.Edu.WebUI.DataAccess.Abstract;
using Yildiz.Edu.WebUI.DataAccess.Concrete;
using Yildiz.Edu.WebUI.DataAccess.Context;
using Yildiz.Edu.WebUI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//builder.Services.AddDbContext<UniEduDbContext>(options =>
//    options.UseSqlServer());

//di container
builder.Services.AddSingleton<IFacultyDal>(new FacultyDal());

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
