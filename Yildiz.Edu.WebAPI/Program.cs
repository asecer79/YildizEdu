using Autofac.Extensions.DependencyInjection;
using Autofac;
using Yildiz.Edu.Business.DependencyResolvers;
using Yildiz.Edu.Security.AuthHelpers;
using Yildiz.Edu.WebUI.AuthHelpers;

var builder = WebApplication.CreateBuilder(args);

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


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yildiz Edu API V1");
    c.RoutePrefix = string.Empty; // Set the Swagger UI at the app's root
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
