using Domain.Clients.DTOs.Request;
using Domain.Clients.DTOs.Response;
using MediatR;

namespace Application.Queries;

public class GetRouteQuery : IRequest<GetRouteResponseDto>
{
    public GetRouteRequestDto Request { get; set; }

    public GetRouteQuery(GetRouteRequestDto request)
    {
        Request = request;
    }
}
