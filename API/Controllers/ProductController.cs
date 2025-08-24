using hahn.Application.product.Commands;
using hahn.Application.product.Queries;
using hahn.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hahn.API.Controllers
{
    [ApiController]
    [Route("product")]
    // [Authorize()]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = nameof(RolesEnum.SELLER))]
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetProducts([FromQuery] int? SellerId,[FromQuery] int? id)
        {
            var result = await _mediator.Send(new GetProductsQuery { sellerId = SellerId, id = id });

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [Authorize(Roles = nameof(RolesEnum.SELLER))]

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteProduct([FromQuery] DeleteProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Datalist);
        }

    }

}