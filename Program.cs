using Microsoft.EntityFrameworkCore;
using SecureSending.Models;
using SecureSending.Services.Account;
using SecureSending.Services.Security;

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
    .AddTransient<IUserService, UserService>()
    .AddTransient<IAccountService, AccountService>()
    .AddTransient<IKeyGenerator, GenerateKey>();

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
