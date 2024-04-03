using Cmd.Application;
using Cms.Endpoints.Web;
using Cms.Infra.Contexts;
using Cms.Infra.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Sinks.MSSqlServer;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

Log.Information("Starting Up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", j =>
    {
        j.Authority = builder.Configuration.GetSection("AuthorityUrl").Value;
        j.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateAudience = false,
            RequireExpirationTime = true
        };
    });

    builder.Services.AddAuthorization(c =>
    {
        c.AddPolicy("myPolicy", c =>
        {
            c.RequireClaim("scope", "api.site");
        });
    });


    builder.Services.AddApplication();

    builder.Services.AddEndpoints();

    builder.Services.AddIdentity<CustomIdentityUser, IdentityRole>().AddEntityFrameworkStores<CmsDbContext>();

    builder.Host.UseSerilog((ctx, lc) =>
    {
        lc.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .WriteTo.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("LogConnectionStrings"),
        sinkOptions: new MSSqlServerSinkOptions { TableName = "WebLogTable", AutoCreateSqlTable = true }).Enrich.FromLogContext();
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseStaticFiles();
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers()/*.RequireAuthorization("myPolicy")*/;

    app.Run();
}
catch (Exception ex)
{

    Log.Fatal(ex, "UnHandled Exception");
}
finally
{
    Log.Information("Shut Down Complated");
    Log.CloseAndFlush();
}