using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace IquiriumBE.Application.Modules.ProductFeedback.Commands.DeleteProductFeedback
{
    public sealed class DeleteProductFeedbackCommand : IRequest<Guid>
    {
        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public Guid ProductFeedbackId { get; set; }
    }
}
