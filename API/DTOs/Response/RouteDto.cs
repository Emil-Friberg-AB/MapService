namespace API.DTOs.Response;

public class RouteDto
{
    public Guid Id { get; set; }
    public List<RouteDetailDto> RouteDetails { get; set; }
    public string Status { get; set; }
}
