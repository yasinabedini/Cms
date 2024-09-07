using Cmd.Application;
using Cmd.Application.Tools.Email;
using Cms.Endpoints.Site;
using Cms.Endpoints.Site.Proxy.Common;
using DotNet8WebAPI.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

Log.Information("Starting Up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder1 => builder1.WithOrigins(builder.Configuration.GetValue<string>("origins")!).AllowAnyMethod().AllowAnyHeader()));


    builder.Services.AddOptions();
    builder.Services.Configure<EmailOp>(
        builder.Configuration.GetSection("MailSettings"));

    builder.Services.Configure<Cms.Infra.Common.Auth.JwtOptions>(
      builder.Configuration.GetSection("Jwt"));


    builder.Services.Configure<Cmd.Application.Security.Auth.JwtOptions>(
      builder.Configuration.GetSection("Jwt"));


    builder.Services.AddHttpClient("FileManager", t =>
    {
        t.BaseAddress = new Uri(builder.Configuration["FileManagerUrl"]);
    });


    builder.Services.AddHttpClient("Asnad", t =>
    {
        t.BaseAddress = new Uri(builder.Configuration.GetSection("AsnadUrl").Value);
    });

    builder.Services.AddHttpClient("Archive", t =>
    {
        t.BaseAddress = new Uri(builder.Configuration.GetSection("ArchiveUrl").Value);
    });



    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(o =>
   {
       var Key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT").GetSection("Secret").Value);
       o.SaveToken = true;
       o.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration.GetSection("JWT").GetSection("Issuer").Value,
           ValidAudience = builder.Configuration.GetSection("JWT").GetSection("Audience").Value,
           IssuerSigningKey = new SymmetricSecurityKey(Key),
           ClockSkew = TimeSpan.Zero
       };
   });

    builder.Services.AddAuthorization();

    builder.Services.AddApplication();

    builder.Services.AddEndpoints();

    builder.Host.UseSerilog((ctx, lc) =>
    {
        lc.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .WriteTo.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("LogConnectionStrings"),
        sinkOptions: new MSSqlServerSinkOptions { TableName = "WebLogTable", AutoCreateSqlTable = true }).Enrich.FromLogContext();
    });


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

    var app = builder.Build();

    app.UseCors("CorsPolicy");


    app.UseMiddleware<CheckAsnadAlready>();

    app.UseMiddleware<JwtMiddleware>();


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