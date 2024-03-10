using Domain.Clients;
using Infrastructure.Services;
using Infrastructure.Settings;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGoogleMapsService, GoogleMapsService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<GoogleMapsConfiguration>(builder.Configuration.GetSection("GoogleMapsConfiguration"));
builder.Services.AddHttpClient<GoogleMapsService>(client =>
{
    var config = builder.Configuration.GetSection("GoogleMapsConfiguration").Get<GoogleMapsConfiguration>();
    client.BaseAddress = new Uri(config.BaseRouteUrl);
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();