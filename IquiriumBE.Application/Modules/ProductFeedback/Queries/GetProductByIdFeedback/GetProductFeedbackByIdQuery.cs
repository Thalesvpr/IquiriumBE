using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IquiriumBE.Application.modules.ProductFeedback.Queries.GetProductFeedback
{
    public sealed class GetProductFeedbackByIdQuery : IRequest<ProductFeedbackEntity>
    {
        [Required(ErrorMessage = "O ID do feedback produto é obrigatório.")]
        public Guid ProductFeedbackId { get; set; }
    }
}

