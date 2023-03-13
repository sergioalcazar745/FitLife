using FitLife.Data;
using FitLife.Repositories;
using Microsoft.EntityFrameworkCore;
using MvcCoreUtilidades.Helpers;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("SqlFitLife");
builder.Services.AddTransient<IRepository, RepositorySQL>();
builder.Services.AddTransient<HelperMail>();
builder.Services.AddDbContext<FitLifeContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAntiforgery();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseResponseCaching();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
);

app.Run();
