using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Features.Queries.GetAllCategories;
using GaezBakeryHouse.Application.Features.Queries.GetProductsByCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GaezBakeryHouse.API.Controllers
{
    [ApiController]
    [Route("api/category")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        readonly IMediator _mediator;

        public CategoryController(IMediator mediator) =>
            _mediator = mediator;

        [HttpGet("GetAllCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            try
            {
                var query = new GetAllCategoriesQuery();
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
