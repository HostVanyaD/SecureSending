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
    .AddControllers();

builder
    .Services
    .AddEndpointsApiExplorer();
    //.AddSwaggerGen();

builder
    .Services
    .AddTransient<IDbRepository, DbRepository>()
    .AddTransient<IGenerateSecureLink, GenerateSecureLink>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app
    .UseHttpsRedirection()
    .UseAuthorization()
    .UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Credentials}/{action=Get}/{uniqueLink?}");

app.MapControllerRoute(
    name: "api",
    pattern: "{controller}/{id?}");

app.Run();
