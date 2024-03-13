namespace Domain.Clients.DTOs.Response;

public class RouteDto
{
    public int DistanceMeters { get; set; }
    public string Duration { get; set; }
    public PolylineDto Polyline { get; set; }
}
