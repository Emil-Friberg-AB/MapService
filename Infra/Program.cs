using Domain.Clients;
using Infra.Services;
using Infra.Settings;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
