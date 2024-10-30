using IquiriumBE.Application.modules.Product.Queries.GetProduct;
using IquiriumBE.Application.Modules.Product.Commands.CreateProduct;
using IquiriumBE.Application.Modules.Product.Commands.DeleteProduct;
using IquiriumBE.Application.Modules.Product.Commands.UpdateProduct;
using IquiriumBE.Application.Modules.Product.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IquiriumBE.Api.Modules.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery { ProductId = id });
            return product == null ? NotFound("Produto não encontrado.") : Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productId = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetProductById), new { id = productId }, productId);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (id != command.ProductId)
            {
                return BadRequest("ID do produto na URL não corresponde ao ID no corpo da requisição.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteProductCommand { ProductId = id }, cancellationToken);
            return NoContent();
        }
    }
}
