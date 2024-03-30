using Cmd.Application;
using Cms.Endpoints.Web;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddEndpoints();

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
    .WriteTo.MSSqlServer(connectionString: "Server=YasiAbdn\\ABDN;Database=CmdLog-Db;TrustServerCertificate=True",
    sinkOptions: new MSSqlServerSinkOptions { TableName = "WebLogTable", AutoCreateSqlTable = true }).Enrich.FromLogContext();
});



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
