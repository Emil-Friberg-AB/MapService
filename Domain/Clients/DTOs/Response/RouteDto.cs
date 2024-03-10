namespace Domain.Clients.DTOs.Response;

public class RouteDto
{
    public BoundsDto Bounds { get; set; }
    public string Copyrights { get; set; }
    public List<LegDto> Legs { get; set; }
    public PolylineDto OverviewPolyline { get; set; }
    public string Summary { get; set; }
    public List<string> Warnings { get; set; }
    public List<int> WaypointOrder { get; set; }
}
