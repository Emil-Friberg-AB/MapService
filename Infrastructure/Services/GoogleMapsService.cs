using System.Text;
using System.Text.Json;
using AutoMapper;
using Domain.Clients;
using Domain.Clients.DTOs.Request;
using Domain.Clients.DTOs.Response;
using Domain.Models;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Route = Domain.Models.Route;

namespace Infrastructure.Services;

public class GoogleMapsService : IGoogleMapsService
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;
    private readonly GoogleMapsConfiguration _config;
    private readonly ILogger<GoogleMapsService> _logger;

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public GoogleMapsService(HttpClient httpClient, 
        IMapper mapper,
        IOptions<GoogleMapsConfiguration> config,
        ILogger<GoogleMapsService> logger)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _config = config.Value;
        _logger = logger;
    }

    public async Task<GetRouteResponseDto> GetRouteAsync(GetRouteRequestDto request)
    {
        _logger.LogInformation("GoogleMapsClient GetRoute for lng {longitude} and lat {latitude}", request.Destination.Location.Longitude, request.Destination.Location.Latitude);
        var url = string.Format(_config.ComputeRouteUrl, _config.ApiKey);
        var jsonContent = JsonConvert.SerializeObject(request);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, httpContent);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("ERROR! GoogleMapsClient GetRoute for lng {longitude} and lat {latitude}", request.Destination.Location.Longitude, request.Destination.Location.Latitude);
            return null;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var routeDto = JsonConvert.DeserializeObject<GetRouteResponseDto>(jsonResponse);
        if(routeDto == null)
        {
            _logger.LogError("ERROR! GoogleMapsClient GetRoute for lng {longitude} and lat {latitude}", request.Destination.Location.Longitude, request.Destination.Location.Latitude);
            return null;
        }

        _logger.LogInformation("GoogleMapsClient Route successful collected with status {status} ", routeDto.Status);

        return routeDto;
    }

    public async Task<List<PointOfInterest>> GetPointsOfInterest(Location location, double radius)
    {
        // Send an HTTP request to the Places API
        var response = await _httpClient.GetAsync($"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={location}&radius={radius}");

        //// Parse the response
        //var data = await response.Content.ReadAsStringAsync();
        //// Convert the data into a list of PointOfInterest models
        //var pointsOfInterest = data.Results.Select(result => new PointOfInterest
        //{
        //    Id = result.Id,
        //    Name = result.Name,
        //    Type = result.Types.FirstOrDefault(),
        //    Location = new Location
        //    {
        //        Latitude = result.Geometry.Location.Lat,
        //        Longitude = result.Geometry.Location.Lng,
        //    },
        //}).ToList();

        var mockPointsOfInterest = new List<PointOfInterest>
{
    new PointOfInterest
    {
        Id = "1",
        Name = "Point 1",
        Type = "Type 1",
        Location = new Location
        {
            Latitude = 12.34,
            Longitude = 56.78,
        },
    },
    new PointOfInterest
    {
        Id = "2",
        Name = "Point 2",
        Type = "Type 2",
        Location = new Location
        {
            Latitude = 90.12,
            Longitude = 34.56,
        },
    },
    // Add more points of interest as needed
};

        return mockPointsOfInterest;
    }
}
