using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Features.Commands.DeleteAllShoppingCartItemsByUserId;
using GaezBakeryHouse.Application.Features.Commands.DeleteShoppingCartItem;
using GaezBakeryHouse.Application.Features.Commands.PostShoppingCartItem;
using GaezBakeryHouse.Application.Features.Queries.GetShoppingCartItemsByUserId;
using GaezBakeryHouse.Application.Features.Queries.GetUserTotalAmount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GaezBakeryHouse.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/shoppingCartItem")]
    public class ShoppingCartItemController : ControllerBase
    {
        readonly IMediator _mediator;

        public ShoppingCartItemController(IMediator mediator) =>
            _mediator = mediator;

        [HttpPost("PostShoppingCartItem")]
        public async Task<ActionResult> PostShoppingCartItem([FromBody] PostShoppingCartItemCommand command)
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

        [HttpGet("GetShoppingCartItemsByUserIdQuery/{userId}")]
        public async Task<ActionResult<IEnumerable<ShoppingCartItemDTO>>> GetShoppingCartItemsByUserIdQuery([FromRoute] string userId)
        {
            try
            {
                var command = new GetShoppingCartItemsByUserIdQuery(userId);
                var response = await _mediator.Send(command);

                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorReponseDTO { Message = ex.Message };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpDelete("DeleteShoppingCartItem/{id:int}/{productId:int}/{applicationUserId}")]
        public async Task<ActionResult> DeleteShoppingCartItem([FromRoute] int id, [FromRoute] int productId, [FromRoute] string applicationUserId)
        {
            try
            {
                var command = new DeleteShoppingCartItemCommand(id, productId, applicationUserId);
                await _mediator.Send(command);

                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorReponseDTO { Message = ex.Message };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpDelete("DeleteAllShoppingCartItemsByUserId/{applicationUserId}")]
        public async Task<ActionResult> DeleteAllShoppingCartItemsByUserId([FromRoute] string applicationUserId)
        {
            try
            {
                var command = new DeleteAllShoppingCartItemsByUserIdCommand(applicationUserId);
                await _mediator.Send(command);

                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorReponseDTO { Message = ex.Message };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpGet("GetUserTotalAmount/{userId}")]
        public async Task<ActionResult<decimal>> GetUserTotalAmount([FromRoute] string userId)
        {
            try
            {
                var command = new GetUserTotalAmountQuery(userId);
                var response = await _mediator.Send(command);

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
