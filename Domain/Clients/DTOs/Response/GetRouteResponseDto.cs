namespace Domain.Clients.DTOs.Response;

public class GetRouteResponseDto
{
    public List<RouteDto> Routes { get; set; }
    public string Status { get; set; }
}
