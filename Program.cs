using CodeFirstApproach.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


// DbContext
builder.Services.AddDbContext<ApplicationDbContext>
    (
        options => options.UseSqlServer
        (
            builder.Configuration.GetConnectionString("dbconn")
        )
    );

// Session
builder.Services.AddSession
    (
        option =>
        {
            option.IdleTimeout = TimeSpan.FromMinutes(5);
            option.Cookie.HttpOnly = true;
            option.Cookie.IsEssential = true;

        }
    );

// HttpContextAccessor
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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=SignIn}/{id?}");

app.Run();
