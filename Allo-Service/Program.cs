using Allo_Service.Models;
using Allo_Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<MyContext>(opt =>
//{
//    opt.UseSqlServer(@"Data source = DESKTOP-TK41FB1\SQLEXPRESS;
//    Encrypt=false; initial catalog=Allo_Service0809;
//    integrated security = true "); 
//});

builder.Services.AddDbContext<MyContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddSession(options =>
//{    
//    // Set session timeout
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.Name = "MySession"; // essential for GDPR compliance
//});

var sessionSettings = builder.Configuration.GetSection("SessionSettings");

builder.Services.AddSession(options =>
{
    // Read session timeout from configuration
    options.IdleTimeout = TimeSpan.FromMinutes(sessionSettings.GetValue<int>("IdleTimeoutMinutes"));

    // Read HttpOnly setting from configuration
    options.Cookie.HttpOnly = sessionSettings.GetValue<bool>("CookieHttpOnly");

    // Read the cookie name from configuration
    options.Cookie.Name = sessionSettings.GetValue<string>("CookieName");
});


builder.Services.AddSignalR();
builder.Services.AddScoped<NotificationHub>();

// Register EmailService
// Choose the appropriate lifetime based on your needs:
builder.Services.AddTransient<EmailService>(); // Or AddScoped/AddSingleton

builder.Services.AddHttpContextAccessor();


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

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Accueil}/{id?}");

app.MapHub<NotificationHub>("/notificationHub");

app.Run();
