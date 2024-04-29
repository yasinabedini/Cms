using Cms.Clients.AdminPanel.Data;
using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvcCore();

builder.Services.AddRazorPages();

builder.Services.AddHttpClient("AdminApi", option =>
{
    option.BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrl").Value);
});

builder.Services.AddHttpClient("FileManager", option =>
{
    option.BaseAddress = new Uri(builder.Configuration.GetSection("FileManagerUrl").Value);
});


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

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddIdentity<CustomIdentityUser, CustomIdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization();

//JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
//builder.Services.AddAuthentication(c =>
//{
//    c.DefaultScheme = "Cookies";
//    c.DefaultChallengeScheme = "oidc";
//}).AddCookie("Cookies");
//.AddOpenIdConnect("oidc", options =>
//{
//    options.Authority = builder.Configuration.GetSection("AuthorityUrl").Value;
//    options.MetadataAddress = builder.Configuration.GetSection("AuthorityUrl").Value + ".well-known/openid-configuration";
//    options.ClientId = "adminPanel";
//    options.ClientSecret = "secret";
//    options.ResponseType = "code";

//    options.MapInboundClaims = false;
//    options.SaveTokens = true;
//    options.RequireHttpsMetadata = false;

//    options.Scope.Clear();
//    options.Scope.Add("openid");
//    options.Scope.Add("profile");
//    options.Scope.Add("api.admin");
//    options.Scope.Add("offline_access");
//});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute().RequireAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
