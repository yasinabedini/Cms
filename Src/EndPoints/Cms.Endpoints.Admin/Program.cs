using System.Text;
using Cmd.Application;
using Cms.Endpoints.Admin;
using Cms.Endpoints.Admin.Token;
using Cms.Infra.Contexts;
using Cms.Infra.Identity.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using static System.Net.Mime.MediaTypeNames;


Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

Log.Information("Starting Up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.


    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "JwtBearer";
        options.DefaultChallengeScheme = "JwtBearer";
    }).AddJwtBearer("JwtBearer",options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration.GetSection("Jwt").GetSection("Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("Jwt").GetSection("Audience").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt").GetSection("Key").Value))
        };
    });

    builder.Services.AddAuthorization();

    builder.Services.AddApplication();

    builder.Services.AddEndpoints();

    builder.Services.AddHttpClient("FileManager", t =>
    {
        t.BaseAddress = new Uri(builder.Configuration["FileManagerUrl"]);
    });


    builder.Host.UseSerilog((ctx, lc) =>
    {
        lc.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .WriteTo.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("LogConnectionStrings"),
        sinkOptions: new MSSqlServerSinkOptions { TableName = "AdminLogTable", AutoCreateSqlTable = true }).Enrich.FromLogContext();
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    var app = builder.Build();

    app.UseMiddleware<CheckJwtAlready>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        if (bool.Parse(builder.Configuration["SwaggerInProduction"]))
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // using static System.Net.Mime.MediaTypeNames;
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
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers()/*.RequireAuthorization()*/;

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
