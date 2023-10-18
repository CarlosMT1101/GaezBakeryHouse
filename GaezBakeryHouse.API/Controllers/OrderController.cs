using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Features.Commands.PostCategory;
using GaezBakeryHouse.Application.Features.Commands.PostOrder;
using GaezBakeryHouse.Application.Features.Queries.GetAllCategories;
using GaezBakeryHouse.Application.Features.Queries.GetOrderDetail;
using GaezBakeryHouse.Application.Features.Queries.GetOrdersByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GaezBakeryHouse.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        readonly IMediator _mediator;

        public OrderController(IMediator mediator) =>
            _mediator = mediator;

        [HttpPost("PostOrder")]
        public async Task<ActionResult> PostOrder(PostOrderCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorReponseDTO { Message = ex.Message };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpGet("GetOrdersByUser/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByUser([FromRoute] string userId)
        {
            try
            {
                var query = new GetOrdersByUserQuery(userId);
                var response = await _mediator.Send(query);

                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorReponseDTO { Message = ex.Message };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpGet("GetOrderDetail/{orderId:int}")]
        public async Task<ActionResult<IEnumerable<OrderDetailDTO>>> GetOrderDetail([FromRoute] int orderId)
        {
            try
            {
                var query = new GetOrderDetailQuery(orderId);
                var response = await _mediator.Send(query);

                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorReponseDTO { Message = ex.Message };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }
    }
}
