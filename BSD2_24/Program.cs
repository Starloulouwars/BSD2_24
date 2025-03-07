using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BSD2_24.Data;
using BSD2_24.Models;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.IOTimeout = TimeSpan.FromDays(1); 
    options.Cookie.Name = ".Ticket.Session";
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Path = "/";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<BSD2_24Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BSD2_24Context") ?? throw new InvalidOperationException("Connection string 'BSD2_24Context' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
