using Domain.Clients.DTOs.Request;
using MediatR;
using Route = Domain.Models.Route;

namespace Application.Queries;

public class GetRouteQuery : IRequest<Route>
{
    public GetRouteRequestDto Request { get; set; }

    public GetRouteQuery(GetRouteRequestDto request)
    {
        Request = request;
    }
}
