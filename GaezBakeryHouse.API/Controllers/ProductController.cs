﻿using GaezBakeryHouse.Application.DTOs;
using GaezBakeryHouse.Application.Features.Commands.DeleteProductCommand;
using GaezBakeryHouse.Application.Features.Commands.PostProductCommand;
using GaezBakeryHouse.Application.Features.Commands.UpdateProductCommand;
using GaezBakeryHouse.Application.Features.Queries.GetProductsByCategory;
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
                var command = new GetProductsByCategoryQuery(categoryId);
                var response = await _mediator.Send(command);

                return StatusCode((int)HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                var errorResponse = new ErrorReponseDTO { Message = ex.Message };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
        }

        [HttpPost("PostProduct")]
        public async Task<ActionResult> PostProduct([FromForm] PostProductCommand command)
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

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct([FromForm] UpdateProductCommand command)
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

        [HttpDelete("DeleteProduct/{id:int}")]
        public async Task<ActionResult> DeleteProduct([FromRoute]int id)
        {
            try
            {
                var command = new DeleteProductCommand(id);
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
