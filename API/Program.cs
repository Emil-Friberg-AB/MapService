using Application.Queries;
using Serilog;
using Domain.Clients;
using Mapster;
using FastExpressionCompiler;
using Infra.Services;
using Infra.Settings;
using Domain.MappingConfiguration;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

var config = TypeAdapterConfig.GlobalSettings;
config.Compiler = exp => exp.CompileFast();
config.Scan(typeof(Program).Assembly);
builder.Services.AddControllers();

RouteConfiguration.Configure();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetRouteQueryHandler).Assembly));
builder.Services.AddScoped<IGoogleMapsService, GoogleMapsService>();
builder.Services.Configure<GoogleMapsConfiguration>(builder.Configuration.GetSection("GoogleMapsConfiguration"));
builder.Services.AddHttpClient<GoogleMapsService>(client =>
{
    var config = builder.Configuration.GetSection("GoogleMapsConfiguration").Get<GoogleMapsConfiguration>();
    client.BaseAddress = new Uri(config.BaseRouteUrl);
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SlowDrive API", Version = "v1" });
});
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
});

// Build the application
var app = builder.Build();

// Add routing and endpoint handling
app.UseRouting();

app.MapControllers();

app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SlowDrive API V1");
    c.RoutePrefix = string.Empty; // Serve Swagger UI at application's root

});

app.Run();
