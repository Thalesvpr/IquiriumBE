using MediatR;
using System.ComponentModel.DataAnnotations;

namespace IquiriumBE.Application.modules.Product.Queries.GetProduct
{
    public sealed class GetProductByIdQuery : IRequest<ProductEntity>
    {
        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public Guid ProductId { get; set; }
    }
}

