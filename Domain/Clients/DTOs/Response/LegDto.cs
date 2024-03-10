using Domain.Clients.DTOs.Shared;

namespace Domain.Clients.DTOs.Response;

public class LegDto
{
    public DistanceDto Distance { get; set; }
    public DurationDto Duration { get; set; }
    public string EndAddress { get; set; }
    public LatLngDto EndLocation { get; set; }
    public string StartAddress { get; set; }
    public LatLngDto StartLocation { get; set; }
    public List<StepDto> Steps { get; set; }
}
