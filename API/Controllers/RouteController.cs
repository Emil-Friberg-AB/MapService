using Application.Queries;
using Domain.Clients.DTOs.Request;
using Domain.Clients.DTOs.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RouteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RouteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<GetRouteResponseDto>> GetRoute(GetRouteRequestDto request)
        {
            var query = new GetRouteQuery(request);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}