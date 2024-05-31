using Cms.Endpoints.AdminPanel.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddRazorPages(t =>
{
    t.Conventions.AddPageRoute("/Dashboard/Analytics", "");
});

#region APIs
builder.Services.AddHttpClient("AdminApi", option =>
{
    option.BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrl").Value);
});

builder.Services.AddHttpClient("FileManager", option =>
{
    option.BaseAddress = new Uri(builder.Configuration.GetSection("FileManagerUrl").Value);
});
#endregion

#region Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = new PathString("/Account/Login");
    options.LogoutPath = new PathString("/Account/Logout");
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
    options.Cookie = new CookieBuilder()
    {
        SecurePolicy = CookieSecurePolicy.Always,
        HttpOnly = true,
        SameSite = SameSiteMode.None
    };
});
#endregion

#region Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddIdentity<CustomIdentityUser, CustomIdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

#endregion

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
