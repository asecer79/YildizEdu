using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");




    //endpoints.MapControllerRoute(
    //    name: "routeV1",
    //    pattern: "{action}/{controller}/{id?}");

    //endpoints.MapControllerRoute(
    //    name: "routeV2",
    //    pattern: "hr/{action}/{id?}",
    //    defaults: new { controller = "Home" });

    //endpoints.MapControllerRoute(
    //    name: "routeV3",
    //    pattern: "hakkinda/{id?}",
    //    defaults: new { controller = "Home", action ="Privacy" });
});

app.Run();
