using Cmd.Application;
using Cms.Endpoints.Site;
using Cms.Infra.Contexts;
using Cms.Infra.Identity.Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using static System.Net.Mime.MediaTypeNames;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

Log.Information("Starting Up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddHttpClient("FileManager", t =>
    {
        t.BaseAddress = new Uri(builder.Configuration["FileManagerUrl"]);
    });


    builder.Services.AddHttpClient("Asnad", t =>
    {
        t.BaseAddress = new Uri(builder.Configuration.GetSection("AsnadUrl").Value);
    });


    // Add services to the container.


    builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer", option =>
    {
        option.Authority = builder.Configuration.GetSection("AuthorityUrl").Value;
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            SaveSigninToken = true,
            ValidateAudience = false
        };
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


    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

               
                context.Response.ContentType = Text.Plain;

                await context.Response.WriteAsync("An exception was thrown.");

                var exceptionHandlerPathFeature =
                    context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                {
                    await context.Response.WriteAsync(" The file was not found.");
                }

                if (exceptionHandlerPathFeature?.Path == "/")
                {
                    await context.Response.WriteAsync(" Page: Home.");
                }
            });
        });
    }

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
    
    app.UseHttpsRedirection();
    
    app.UseAuthentication();
    app.UseAuthorization();    
    app.MapControllers();

    app.Run();
}

catch (Exception ex)
{

    Log.Fatal(ex, $"UnHandled Exception:{ex.Message}");
}
finally
{
    Log.Information("Shut Down Complated");
    Log.CloseAndFlush();
}