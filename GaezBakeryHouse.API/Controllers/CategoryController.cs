using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Features.Commands.DeleteCategoryCommand;
using GaezBakeryHouse.Application.Features.Commands.PostCategoryCommand;
using GaezBakeryHouse.Application.Features.Commands.UpdateCategoryCommand;
using GaezBakeryHouse.Application.Features.Queries.GetAllCategories;
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
            catch (Exception ex)
            {
                var errorResponse = new ErrorReponseDTO { Message =  ex.Message };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpPost("PostCategory")]
        public async Task<ActionResult> PostCategory([FromForm] PostCategoryCommand command)
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

        [HttpPut("UpdateCategory")]
        public async Task<ActionResult> UpdateCategory([FromForm] UpdateCategoryCommand command)
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

        [HttpDelete("DeleteCategory/{id:int}")]
        public async Task<ActionResult> DeleteCategory([FromRoute] int id)
        {
            try
            {
                var command = new DeleteCategoryCommand(id);
                await _mediator.Send(command);

                return StatusCode((int)HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorReponseDTO { Message = ex.Message };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }
    }
}
