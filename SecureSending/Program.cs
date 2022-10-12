using Microsoft.EntityFrameworkCore;
using SecureSending.Services.Data;
using SecureSending.Services.Security;
using SecureSending.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder
    .Services
    .AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));

builder
    .Services
    .AddControllersWithViews();

builder
    .Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder
    .Services
    .AddTransient<IDbService, DbService>()
    .AddTransient<IGenerateSecureLink, GenerateSecureLink>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseAuthorization()
    .UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "api",
    pattern: "{controller}/{id?}");

app.Run();
