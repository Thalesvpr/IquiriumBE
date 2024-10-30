using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IquiriumBE.Application.Modules.Product.Commands.CreateProduct
{
    public sealed class CreateProductCommand : IRequest<Guid>
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O título deve ter no máximo 255 caracteres.")]
        public required string Title { get; set; }

        [MaxLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        [MaxLength(50, ErrorMessage = "O status deve ter no máximo 50 caracteres.")]
        public required string Status { get; set; }
    }
}
