using Domain.Clients.DTOs.Request;
using Domain.Clients.DTOs.Response;
using Domain.Models;
using Route = Domain.Models.Route;

namespace Domain.Clients;

public interface IGoogleMapsService
{
    Task<GetRouteResponseDto> GetRouteAsync(GetRouteRequestDto request);
    Task<List<PointOfInterest>> GetPointsOfInterest(Location location, double radius);
}
