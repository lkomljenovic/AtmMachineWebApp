using AtmMachine.DAL;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dodavanje dbContext-a u .NET Core 6
builder.Services.AddDbContext<AtmMachineDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("AtmMachineDbContext")));
    // Commented out old database
    //options.UseSqlServer(
    //    builder.Configuration.GetConnectionString("AtmMachineDbContext")));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

    

app.Run();
