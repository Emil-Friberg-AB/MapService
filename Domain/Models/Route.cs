namespace Domain.Models;

public class Route
{
    public Route()
    {
        RouteDetails = new List<RouteDetail>();
    }
    public Guid Id { get; set; } = Guid.NewGuid();
    public List<RouteDetail> RouteDetails { get; set; }
    public string Status { get; set; }
}
