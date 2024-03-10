using Domain.Clients.DTOs.Shared;

namespace Domain.Clients.DTOs.Response;

public class BoundsDto
{
    public LatLngDto Northeast { get; set; }
    public LatLngDto Southwest { get; set; }
}
