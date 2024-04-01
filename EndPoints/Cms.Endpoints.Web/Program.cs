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

    builder.Services.AddApplication();

    builder.Services.AddEndpoints();

    builder.Services.AddIdentity<CustomIdentityUser, IdentityRole>().AddEntityFrameworkStores<CmsDbContext>();

    builder.Host.UseSerilog((ctx, lc) =>
    {
        lc.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .WriteTo.MSSqlServer(connectionString: "Server=YasiAbdn\\ABDN;Database=CmsLog-Db;Integrated security=true;TrustServerCertificate=True",
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

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

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