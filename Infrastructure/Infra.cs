using Domain.Clients;
using Infra.Services;
using Infra.Settings;
using Mapster;

namespace Infra;
public static class Infra
{
    public static void InitInfrastructure(this WebApplicationBuilder builder, TypeAdapterConfig config)
    {
        config.Scan(typeof(Infra).Assembly);
        builder.Services.AddScoped<IGoogleMapsService, GoogleMapsService>();
        builder.Services.Configure<GoogleMapsConfiguration>(builder.Configuration.GetSection("GoogleMapsConfiguration"));
        builder.Services.AddHttpClient<GoogleMapsService>(client =>
        {
            var config = builder.Configuration.GetSection("GoogleMapsConfiguration").Get<GoogleMapsConfiguration>();
            client.BaseAddress = new Uri(config.BaseRouteUrl);
        });
    }
}
