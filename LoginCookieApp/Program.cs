using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/login";//this will redirect to login page after click on forLoggedUsers (if not logged)
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseStaticFiles();//affects looks of nav bar on the page

app.UseRouting();
app.UseAuthentication();//cookie will get created
app.UseAuthorization();//gives return url when clicked on link to endpoint with attribute [Authorize]

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
