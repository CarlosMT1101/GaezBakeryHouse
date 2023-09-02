using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Features.Queries.GetProductsByCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GaezBakeryHouse.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        readonly IMediator _mediator;

        public ProductController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("GetProductsByCategory/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory([FromRoute]int categoryId)
        {
            var query = new GetProductsByCategoryQuery(categoryId);

            var products = await _mediator.Send(query);

            return Ok(products);
        }
    }
}
