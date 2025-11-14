using Microsoft.EntityFrameworkCore;
using HospitalSystem.Models;
using HospitalSystem.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HospitalDbContext>(opts => {
	opts.UseSqlite(builder.Configuration["ConnectionStrings:HospitalConnection"]);
});

builder.Services.AddScoped<IHospitalRepository, EFHospitalRepository>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
	options.Cookie.Name = ".HospitalSystem.Session";
});

var app = builder.Build();

// SeedData.EnsurePopulated(app);

app.UseStaticFiles();

app.UseSession();

app.MapDefaultControllerRoute();

app.Run();
