using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Features.Queries.GetProductsByCategory;
using GaezBakeryHouse.Application.Features.Queries.GetProductsInOffer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GaezBakeryHouse.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        readonly IMediator _mediator;

        public ProductController(IMediator mediator) =>
            _mediator = mediator;

        [HttpGet("GetProductsByCategory/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory([FromRoute] int categoryId)
        {
            try
            {
                var query = new GetProductsByCategoryQuery(categoryId);
                var response = await _mediator.Send(query);

                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (Exception)
            {
                var errorResponse = new ErrorReponseDTO { Message = "Algo salió mal" };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpGet("GetProductsInOffer")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsInOffer()
        {
            try
            {
                var query = new GetProductsInOfferQuery();
                var response = await _mediator.Send(query);

                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (Exception)
            {
                var errorResponse = new ErrorReponseDTO { Message = "Algo salió mal" };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }
    }
}
