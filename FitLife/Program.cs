using FitLife.Data;
using FitLife.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("SqlFitLife");
builder.Services.AddTransient<IRepository, RepositorySQL>();
builder.Services.AddDbContext<FitLifeContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
);

app.Run();
