using IdentityModel;
using IdentityServer4;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient("AdminApi", option =>
{
    option.BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrl").Value);
});
builder.Services.AddHttpClient("FileManager", option =>
{
    option.BaseAddress = new Uri(builder.Configuration.GetSection("FileManagerUrl").Value);
});


JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
builder.Services.AddAuthentication(c =>
{    
    c.DefaultScheme = "Cookies";
    c.DefaultChallengeScheme = "oidc";
}).AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
{
    options.Authority = builder.Configuration.GetSection("AuthorityUrl").Value;
    options.ClientId = "adminPanel";
    options.ClientSecret = "secret";
    options.ResponseType = "code";

    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;    

    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("api.admin");
    options.Scope.Add("offline_access");
});

builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
