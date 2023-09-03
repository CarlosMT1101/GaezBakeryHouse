using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Features.Queries.GetProductsByCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var query = new GetProductsByCategoryQuery(categoryId);

            var products = await _mediator.Send(query);

            return Ok(products);
        }
    }
}
