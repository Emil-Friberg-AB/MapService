using API.DTOs.Request;
using API.DTOs.Response;
using Application.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Route = Domain.Models.Route;

namespace API.Controllers;

[ApiController]
    [Route("[controller]")]
    public class RouteController : BaseController<RouteController>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RouteController> _logger;

        public RouteController(IMediator mediator, ILogger<RouteController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("RouteController is running");
        }

        [HttpPost]
        [ProducesResponseType(typeof(RouteDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetRoute([FromBody] GetRouteRequestDto request)
        {
            return await TryExecuteAsync<IActionResult>(async () =>
            {
                var routeQueryRequest = request.Adapt<Domain.Clients.DTOs.Request.GetRouteRequestDto>();
                var query = new GetRouteQuery(routeQueryRequest);
                var result = (await _mediator.Send(query)).Adapt<RouteDto>();
                return Ok(new BaseResponseDto<RouteDto>
                {
                    Result = result
                });
            }, _logger);
        }
    }
