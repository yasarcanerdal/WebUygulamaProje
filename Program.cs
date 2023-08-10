using Microsoft.EntityFrameworkCore;
using WebUygulamaProje.Context;
using WebUygulamaProje.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
// RazorPageslerı jullanabilmemiz için program cs bildirmeliyiz (hazır kütüphaneden gelen işlemleri)
builder.Services.AddRazorPages();

// _bookTypeRepository nesnesi => Dependency Injection (Olu�turulmas�na sa�l�yor.)
builder.Services.AddScoped<IBookTypeRepository, BookTypeRepository>();
// bookRepository nesnesi => Dependency Injection (Olu�turulmas�na sa�l�yor.)
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IHireRepository, HireRepository>();

builder.Services.AddScoped<IEmailSender, EmailSender>();


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

app.MapRazorPages();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
