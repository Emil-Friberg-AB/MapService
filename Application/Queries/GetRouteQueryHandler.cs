using Domain.Clients.DTOs.Response;
using Domain.Clients;
using MediatR;

namespace Application.Queries;

public class GetRouteQueryHandler : IRequestHandler<GetRouteQuery, GetRouteResponseDto>
{
    private readonly IGoogleMapsService _googleMapsService;

    public GetRouteQueryHandler(IGoogleMapsService googleMapsService)
    {
        _googleMapsService = googleMapsService;
    }

    public async Task<GetRouteResponseDto> Handle(GetRouteQuery request, CancellationToken cancellationToken)
    {
        var responseDto = await _googleMapsService.GetRouteAsync(request.Request);

        if(responseDto == null)
        { 
            return new GetRouteResponseDto
            {
                Status = "ERROR"
            };
        }
        return responseDto;
    }
}
