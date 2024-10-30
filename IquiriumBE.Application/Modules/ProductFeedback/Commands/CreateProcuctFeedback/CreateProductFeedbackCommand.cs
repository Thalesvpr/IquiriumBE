using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IquiriumBE.Application.Modules.ProductFeedback.Commands.CreateProductFeedback
{
    public sealed class CreateProductFeedbackCommand : IRequest<Guid>
    {
        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public required Guid ProductId { get; set; }

        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public required string UserId { get; set; }

        [Required(ErrorMessage = "A nota é obrigatória.")]
        [Range(1, 5, ErrorMessage = "A nota deve estar entre 1 e 5.")]
        public int Rating { get; set; }

        [MaxLength(1000, ErrorMessage = "O feedback deve ter no máximo 1000 caracteres.")]
        public string? Feedback { get; set; }
    }
}
