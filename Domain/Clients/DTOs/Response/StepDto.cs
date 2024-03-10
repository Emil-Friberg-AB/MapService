using Domain.Clients.DTOs.Shared;

namespace Domain.Clients.DTOs.Response;

public class StepDto
{
    public DistanceDto Distance { get; set; }
    public DurationDto Duration { get; set; }
    public LatLngDto EndLocation { get; set; }
    public string HtmlInstructions { get; set; }
    public PolylineDto Polyline { get; set; }
    public LatLngDto StartLocation { get; set; }
    public string TravelMode { get; set; }
}
