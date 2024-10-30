using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Threading.Tasks;
using IquiriumBE.Application.Modules.ProductFeedback.Commands.CreateProductFeedback;
using IquiriumBE.Application.Modules.ProductFeedback.Queries.GetProductFeedbacks;
using System.Security.Claims;

namespace IquiriumBE.Api.Modules.ProductFeedback
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductFeedbackController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductFeedbackController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var query = new GetProductFeedbacksQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateProductFeedbackCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.UserId = userId!; // [Authorize]
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
