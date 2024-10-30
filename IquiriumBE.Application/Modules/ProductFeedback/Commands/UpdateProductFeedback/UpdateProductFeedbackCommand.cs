using System.ComponentModel.DataAnnotations;
using MediatR;

namespace IquiriumBE.Application.Modules.ProductFeedback.Commands.UpdateProductFeedback
{
    public sealed class UpdateProductFeedbackCommand : IRequest<Guid>
    {
        [Required(ErrorMessage = "O ID do feedback do produto é obrigatório.")]
        public required Guid ProductFeedbackId { get; set; }

        [Required(ErrorMessage = "A nota é obrigatória.")]
        [Range(1, 5, ErrorMessage = "A nota deve estar entre 1 e 5.")]
        public int Rating { get; set; }

        [MaxLength(1000, ErrorMessage = "O feedback deve ter no máximo 1000 caracteres.")]
        public string? Feedback { get; set; }
    }
}
