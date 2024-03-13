using Domain.Clients;
using MediatR;
using Mapster;
using Route = Domain.Models.Route;

namespace Application.Queries;

public class GetRouteQueryHandler : IRequestHandler<GetRouteQuery, Route>
{
    private readonly IGoogleMapsService _googleMapsService;

    public GetRouteQueryHandler(IGoogleMapsService googleMapsService)
    {
        _googleMapsService = googleMapsService;
    }

    public async Task<Route> Handle(GetRouteQuery request, CancellationToken cancellationToken)
    {
        var route = (await _googleMapsService.GetRouteAsync(request.Request)).Adapt<Route>();

        if (route == null)
        {
            return new Route
            {
                Status = "ERROR"
            };
        }
        route.Status = "OK";
        return route;
    }
}
